using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsNestedCollections()
 {
 var tripId = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(TripEntity), true)!;
 Set(entity, "Id", tripId);
 Set(entity, "Title", "Trip");
 Set(entity, "Subtitle", "Sub");
 Set(entity, "Description", "Desc");

 var advantage = Activator.CreateInstance(typeof(AdvantageEntity), true)!;
 Set(advantage, "Id", Guid.NewGuid());
 Set(advantage, "Title", "Adv");
 Set(advantage, "Content", "Adv C");

 var highlight = Activator.CreateInstance(typeof(HighlightEntity), true)!;
 Set(highlight, "Id", Guid.NewGuid());
 Set(highlight, "Title", "High");
 Set(highlight, "Icon", "icon");

 var tripImage = Activator.CreateInstance(typeof(TripImageEntity), true)!;
 Set(tripImage, "TripId", tripId);
 Set(tripImage, "IsMain", true);
 Set(tripImage, "Order",1);
 var image = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
 Set(image, "Id", Guid.NewGuid());
 Set(image, "Filename", "i.png");
 Set(image, "BlobUrl", "https://blob");
 Set(image, "BlobPath", "/i");
 Set(image, "Size",1L);
 Set(tripImage, "Image", image);

 var payment = Activator.CreateInstance(typeof(TripPaymentScheduleEntity), true)!;
 Set(payment, "TripId", tripId);
 Set(payment, "Order",2);
 Set(payment, "Title", "Pay");
 Set(payment, "Price", "$1");
 Set(payment, "Description", "P Desc");

 var include = Activator.CreateInstance(typeof(TripPriceIncludeEntity), true)!;
 Set(include, "TripId", tripId);
 Set(include, "Order",3);
 Set(include, "Includes", true);
 Set(include, "Content", "Content");

 var req = Activator.CreateInstance(typeof(TripRequirementEntity), true)!;
 Set(req, "TripId", tripId);
 Set(req, "Order",4);
 Set(req, "Description", "Req");

 var schedule = Activator.CreateInstance(typeof(TripScheduleEntity), true)!;
 Set(schedule, "TripId", tripId);
 Set(schedule, "Order",5);
 Set(schedule, "Title", "Sched");
 Set(schedule, "Content", "Sched C");

 var suggested = Activator.CreateInstance(typeof(TripSuggestedFlightEntity), true)!;
 Set(suggested, "TripId", tripId);
 Set(suggested, "Order",6);
 var image2 = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
 Set(image2, "Id", Guid.NewGuid());
 Set(image2, "Filename", "i2.png");
 Set(image2, "BlobUrl", "https://blob2");
 Set(image2, "BlobPath", "/i2");
 Set(image2, "Size",2L);
 Set(suggested, "Image", image2);

 // collections
 Set(entity, "Advantages", new List<AdvantageEntity>{ (AdvantageEntity)advantage });
 Set(entity, "Highlights", new List<HighlightEntity>{ (HighlightEntity)highlight });
 Set(entity, "Images", new List<TripImageEntity>{ (TripImageEntity)tripImage });
 Set(entity, "PaymentSchedules", new List<TripPaymentScheduleEntity>{ (TripPaymentScheduleEntity)payment });
 Set(entity, "PriceIncludes", new List<TripPriceIncludeEntity>{ (TripPriceIncludeEntity)include });
 Set(entity, "Requirements", new List<TripRequirementEntity>{ (TripRequirementEntity)req });
 Set(entity, "Schedules", new List<TripScheduleEntity>{ (TripScheduleEntity)schedule });
 Set(entity, "SuggestedFlights", new List<TripSuggestedFlightEntity>{ (TripSuggestedFlightEntity)suggested });

 var vm = TripVM.MapFromDomainEntity((TripEntity)entity);

 Assert.Equal(tripId, vm.Id);
 Assert.Equal("Trip", vm.Title);
 Assert.Equal("Sub", vm.Subtitle);
 Assert.Equal("Desc", vm.Description);
 Assert.Single(vm.Advantages);
 Assert.Single(vm.Highlights);
 Assert.Single(vm.Images);
 Assert.Single(vm.PaymentSchedules);
 Assert.Single(vm.PriceIncludes);
 Assert.Single(vm.Requirements);
 Assert.Single(vm.Schedules);
 Assert.Single(vm.SuggestedFlights);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = TripVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}
