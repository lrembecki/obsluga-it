using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Pages.Home;


public record HomeGetAllRequest() : IRequest<HomeVM>
{
    public static Delegate Delegate => (ISender sender, CancellationToken ct) => sender.Send(new HomeGetAllRequest(), ct).ToServiceCallResultAsync();

    internal sealed class Handler(IUnitOfWork uow) : IHandler<HomeGetAllRequest, HomeVM>
    {
        public async Task<HomeVM> Handle(
            HomeGetAllRequest request,
            CancellationToken ct
        )
        {
            var entities = await uow.GetRepository<HomeEntity>().GetAsync();
            var entity = entities.FirstOrDefault();
            return HomeVM.Map(entity!) ?? HomeVM.Empty;
        }
    }
}
