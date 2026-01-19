using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public sealed record AboutUsGetAllRequest() : IRequest<AboutUsVM>
{
    public static Delegate Delegate => (ISender sender, CancellationToken ct) => sender.Send(new AboutUsGetAllRequest(), ct).ToServiceCallResultAsync();

    public static Task<ServiceCallResult<AboutUsVM>> DelegateEndpoint(
        ISender sender,
        CancellationToken cancellationToken = default
    ) =>
        sender.Send(
            new AboutUsGetAllRequest(),
            cancellationToken
        ).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow
    ) : IHandler<AboutUsGetAllRequest, AboutUsVM>
    {

        public async Task<AboutUsVM> Handle(
            AboutUsGetAllRequest request,
            CancellationToken cancellationToken
        )
        {
            var entities = await uow.GetRepository<AboutUsEntity>().GetAsync();
            var entity = AboutUsVM.Map(entities.FirstOrDefault()!)
                ?? AboutUsVM.Empty;

            return entity;
        }
    }
}
