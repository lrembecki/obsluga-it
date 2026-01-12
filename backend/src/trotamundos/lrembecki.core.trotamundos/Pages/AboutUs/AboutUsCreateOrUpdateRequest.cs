using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsCreateOrUpdateRequest(AboutUsDto Model) : IRequest<AboutUsVM>
{
    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<AboutUsCreateOrUpdateRequest, AboutUsVM>
    {
        public async Task<AboutUsVM> Handle(
            AboutUsCreateOrUpdateRequest request,
            CancellationToken cancellationToken
        )
        {
            var entities = await uow.GetRepository<AboutUsEntity>().GetAsync();
            var entity = entities.FirstOrDefault();
            var model = request.Model;
            var persons = model.Persons ?? [];

            if (entity is null)
            {
                model = model with
                {
                    ImageId = (await storage.CreateAsync(model.Image, cancellationToken)).Id
                };

                for (var i = 0; i < persons.Count; i++)
                {
                    var person = persons[i];
                    var image = person.Image;
                    if (image is not null)
                    {
                        var storedImage = await storage.CreateAsync(image, cancellationToken);
                        persons[i] = person with { ImageId = storedImage.Id };
                    }
                }

                entity = await uow.GetRepository<AboutUsEntity>().AddAsync(
                    AboutUsEntity.Create(Guid.NewGuid(), model)
                );
            }
            else
            {
                for (var i = 0; i < persons.Count; i++)
                {
                    var person = persons[i];
                    if (person.ImageId == Guid.Empty)
                    {
                        var storedImage = await storage.CreateAsync(person.Image, cancellationToken);
                        persons[i] = person with { ImageId = storedImage.Id };
                    }
                    else
                    {
                        await storage.UpdateAsync(person.ImageId, person.Image, cancellationToken);
                    }
                }

                await storage.UpdateAsync(entity.ImageId, model.Image, cancellationToken);
                entity.Update(model);
            }

            await uow.SaveChangesAsync(cancellationToken);

            return AboutUsVM.Map(entity);
        }
    }
}
