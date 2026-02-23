using lrembecki.core.Helpers;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips;
using lrembecki.core.trotamundos.Trips.Dtos;
using lrembecki.infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;

namespace lrembecki.function_app;

public class MigrateTripsFunction(
    ILogger<MigrateTripsFunction> logger,
    ITripService tripService,
    ISessionAccessor session,
    IHttpClientFactory httpClientFactory)
{
    private const string SourceUrl = "https://satrotamundos.blob.core.windows.net/data/TrotamundosTourEntity.json";

    [Function("MigrateTripsFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{subscriptionId}/migrate")] HttpRequest req,
        Guid subscriptionId,
        CancellationToken cancellationToken)
    {
        using (var context = session.CreateSessionContext(subscriptionId))
        {
            logger.LogInformation("Starting trip migration from {SourceUrl}", SourceUrl);

            var httpClient = httpClientFactory.CreateClient();
            var json = await httpClient.GetStringAsync(SourceUrl, cancellationToken);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var roots = JsonSerializer.Deserialize<List<SourceRoot>>(json, options) ?? [];

            var sourceTours = roots
                .SelectMany(root => root.Tours ?? [])
                .Select(wrapper => wrapper.Tour)
                .Where(tour => tour != null)
                .DistinctBy(tour => tour!.TourId ?? tour.Name)
                .ToList();

            var existingTrips = await tripService.GetAllAsync(cancellationToken);
            var existingKeys = existingTrips
                .Select(trip => BuildKey(trip.Name, trip.StartDate))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var created = 0;
            var skipped = 0;
            var failed = 0;

            foreach (var tour in sourceTours)
            {
                var tourKey = BuildKey(tour!.Name, ParseDate(tour.StartDate) ?? ParseDate(tour.CalendarPlan?.StartDate));
                if (existingKeys.Contains(tourKey))
                {
                    skipped++;
                    continue;
                }

                try
                {
                    var dto = await BuildTripDtoAsync(tour, httpClient, cancellationToken);
                    await tripService.CreateAsync(dto, cancellationToken);
                    existingKeys.Add(tourKey);
                    created++;
                }
                catch (Exception ex)
                {
                    failed++;
                    logger.LogError(ex, "Failed to migrate trip {TripName}", tour.Name);
                }
            }

            return new OkObjectResult(new
            {
                created,
                skipped,
                failed
            });
        }
    }

    private static async Task<TripDto> BuildTripDtoAsync(
        SourceTour tour,
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        var startDate = ParseDate(tour.StartDate) ?? ParseDate(tour.CalendarPlan?.StartDate);
        var endDate = ParseDate(tour.EndDate) ?? ParseDate(tour.CalendarPlan?.EndDate);

        var agenda = (tour.TaskDescriptions ?? [])
            .Select((item, index) => new TripAgendaDto
            {
                Order = index + 1,
                Content = item.Description ?? string.Empty
            })
            .ToList();

        var highlights = (tour.ReasonDescriptions ?? [])
            .Select((item, index) => new TripHighlightDto
            {
                Order = index + 1,
                HighlightId = Guid.NewGuid(),
                Value = item.Description ?? string.Empty
            })
            .ToList();

        var schedules = (tour.Agenda ?? [])
            .Select((item, index) => new TripScheduleDto
            {
                Order = index + 1,
                Title = string.IsNullOrWhiteSpace(item.Date) ? $"Dzień {item.Day}" : item.Date,
                Content = BuildScheduleContent(item)
            })
            .ToList();

        var requirements = BuildRequirements(tour);
        var paymentSchedules = BuildPaymentSchedules(tour);
        var priceIncludes = BuildPriceIncludes(tour);

        var images = await BuildImagesAsync(tour.Pictures, httpClient, cancellationToken);
        var suggestedFlights = await BuildSuggestedFlightsAsync(tour.SuggestedTravel?.Pictures, httpClient, cancellationToken);

        return new TripDto
        {
            Name = tour.Name,
            IsActive = tour.Active,
            IsDisabled = tour.IsDisabled,
            StartDate = startDate,
            EndDate = endDate,
            Calendar = tour.Calendar?.DisplayName ?? tour.Calendar?.Name,
            Title = tour.Name ?? string.Empty,
            Subtitle = tour.Description ?? string.Empty,
            Description = tour.Description2 ?? tour.Description ?? string.Empty,
            SuggestedFlightNotes = tour.SuggestedTravel?.Description ?? string.Empty,
            Agenda = agenda,
            Highlights = highlights,
            Schedules = schedules,
            Requirements = requirements,
            PaymentSchedules = paymentSchedules,
            PriceIncludes = priceIncludes,
            Images = images,
            SuggestedFlights = suggestedFlights,
            Advantages = [],
            LoyalityPrograms = []
        };
    }

    private static List<TripRequirementDto> BuildRequirements(SourceTour tour)
    {
        var items = new List<string?>
        {
            tour.Description_ForWhom is { Length: > 0 } ? $"Dla kogo: {tour.Description_ForWhom}" : null,
            tour.Description_Group is { Length: > 0 } ? $"Grupa: {tour.Description_Group}" : null,
            tour.Description_Dificulty is { Length: > 0 } ? $"Poziom trudności: {tour.Description_Dificulty}" : null
        };

        if (tour.ImportantItems != null)
        {
            items.AddRange(tour.ImportantItems.Select(item => item.Description));
        }

        return items
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select((item, index) => new TripRequirementDto
            {
                Order = index + 1,
                Description = item!
            })
            .ToList();
    }

    private static List<TripPaymentScheduleDto> BuildPaymentSchedules(SourceTour tour)
    {
        return (tour.PaymentSchedules ?? [])
            .Select(item => new TripPaymentScheduleDto
            {
                Order = item.OrderNumber,
                Title = $"Rata {item.OrderNumber}",
                Price = item.AmountText ?? string.Empty,
                Description = item.Description ?? string.Empty
            })
            .ToList();
    }

    private static List<TripPriceIncludeDto> BuildPriceIncludes(SourceTour tour)
    {
        var result = new List<TripPriceIncludeDto>();

        if (tour.Price?.Include != null)
        {
            result.AddRange(tour.Price.Include
                .Select((item, index) => new TripPriceIncludeDto
                {
                    Order = index + 1,
                    Includes = true,
                    Content = item.Description ?? string.Empty
                }));
        }

        if (tour.Price?.OutOfInclude != null)
        {
            var startIndex = result.Count + 1;
            result.AddRange(tour.Price.OutOfInclude
                .Select((item, index) => new TripPriceIncludeDto
                {
                    Order = startIndex + index,
                    Includes = false,
                    Content = item.Description ?? string.Empty
                }));
        }

        return result;
    }

    private static async Task<List<TripImageDto>> BuildImagesAsync(
        List<SourcePicture>? pictures,
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        if (pictures == null || pictures.Count == 0)
        {
            return [];
        }

        var images = new List<TripImageDto>(pictures.Count);
        for (var i = 0; i < pictures.Count; i++)
        {
            var storage = await DownloadStorageDtoAsync(pictures[i], httpClient, cancellationToken);
            if (storage == null)
            {
                continue;
            }

            images.Add(new TripImageDto
            {
                Order = i + 1,
                Image = storage
            });
        }

        return images;
    }

    private static async Task<List<TripSuggestedFlightDto>> BuildSuggestedFlightsAsync(
        List<SourcePicture>? pictures,
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        if (pictures == null || pictures.Count == 0)
        {
            return [];
        }

        var images = new List<TripSuggestedFlightDto>(pictures.Count);
        for (var i = 0; i < pictures.Count; i++)
        {
            var storage = await DownloadStorageDtoAsync(pictures[i], httpClient, cancellationToken);
            if (storage == null)
            {
                continue;
            }

            images.Add(new TripSuggestedFlightDto
            {
                Order = i + 1,
                Image = storage
            });
        }

        return images;
    }

    private static async Task<StorageDto?> DownloadStorageDtoAsync(
        SourcePicture picture,
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(picture.Url))
        {
            return null;
        }

        var safeUrl = Uri.EscapeUriString(picture.Url);
        var bytes = await httpClient.GetByteArrayAsync(safeUrl, cancellationToken);
        var fileName = GetFileName(picture.Url) ?? $"image-{picture.Id:N}.jpg";
        var mimeType = GetMimeType(fileName);
        var base64 = Convert.ToBase64String(bytes);

        return new StorageDto
        {
            BinaryData = $"data:{mimeType};base64,{base64}",
            Filename = fileName,
            Size = bytes.LongLength,
            Image = new ImageStorageDto
            {
                Width = picture.Width ?? 0,
                Height = picture.Height ?? 0,
                DisplayName = fileName
            }
        };
    }

    private static string BuildScheduleContent(SourceAgenda item)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(item.Description))
        {
            parts.Add(item.Description!);
        }

        if (item.DescriptionItems != null)
        {
            parts.AddRange(item.DescriptionItems
                .Select(detail => detail.Description)
                .Where(detail => !string.IsNullOrWhiteSpace(detail))
                .Select(detail => detail!));
        }

        return string.Join("\n\n", parts);
    }

    private static string? GetFileName(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return null;
        }

        return Path.GetFileName(Uri.UnescapeDataString(uri.LocalPath));
    }

    private static string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => "image/jpeg"
        };
    }

    private static DateTime? ParseDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return DateTime.TryParse(
            value,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal,
            out var date)
            ? date
            : null;
    }

    private static string BuildKey(string? name, DateTime? startDate)
        => $"{name ?? string.Empty}|{startDate:O}";

    private sealed record SourceRoot
    {
        public Guid Id { get; init; }
        public List<SourceTourWrapper>? Tours { get; init; }
    }

    private sealed record SourceTourWrapper
    {
        public SourceTour? Tour { get; init; }
    }

    private sealed record SourceTour
    {
        public string? TourId { get; init; }
        public string? Name { get; init; }
        public string? StartDate { get; init; }
        public string? EndDate { get; init; }
        public string? Description { get; init; }
        public string? Description2 { get; init; }
        public bool Active { get; init; }
        public bool IsDisabled { get; init; }
        public string? Description_Date { get; init; }
        public string? Description_Group { get; init; }
        public string? Description_Dificulty { get; init; }
        public string? Description_ForWhom { get; init; }
        public List<SourcePicture>? Pictures { get; init; }
        public List<SourceText>? TaskDescriptions { get; init; }
        public List<SourceText>? ReasonDescriptions { get; init; }
        public List<SourceAgenda>? Agenda { get; init; }
        public SourcePrice? Price { get; init; }
        public SourceSuggestedTravel? SuggestedTravel { get; init; }
        public List<SourcePaymentSchedule>? PaymentSchedules { get; init; }
        public List<SourceText>? ImportantItems { get; init; }
        public SourceCalendar? Calendar { get; init; }
        public SourceCalendarPlan? CalendarPlan { get; init; }
    }

    private sealed record SourceCalendar
    {
        public string? Name { get; init; }
        public string? DisplayName { get; init; }
    }

    private sealed record SourceCalendarPlan
    {
        public string? StartDate { get; init; }
        public string? EndDate { get; init; }
    }

    private sealed record SourceSuggestedTravel
    {
        public string? Description { get; init; }
        public List<SourcePicture>? Pictures { get; init; }
    }

    private sealed record SourcePrice
    {
        public List<SourceText>? Include { get; init; }
        public List<SourceText>? OutOfInclude { get; init; }
    }

    private sealed record SourcePaymentSchedule
    {
        public int OrderNumber { get; init; }
        public string? AmountText { get; init; }
        public string? Description { get; init; }
    }

    private sealed record SourceAgenda
    {
        public int Day { get; init; }
        public string? Date { get; init; }
        public string? Description { get; init; }
        public List<SourceText>? DescriptionItems { get; init; }
    }

    private sealed record SourceText
    {
        public string? Description { get; init; }
    }

    private sealed record SourcePicture
    {
        public Guid Id { get; init; }
        public string? Url { get; init; }
        public long? Width { get; init; }
        public long? Height { get; init; }
    }
}