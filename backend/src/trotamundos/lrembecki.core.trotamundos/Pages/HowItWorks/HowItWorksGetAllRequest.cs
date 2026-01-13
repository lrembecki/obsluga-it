using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public sealed record HowItWorksGetAllRequest() : IRequest<HowItWorksVM>
{
    public static Task<ServiceCallResult<HowItWorksVM>> DelegateEndpoint(
        ISender sender,
        CancellationToken cancellationToken = default
    ) =>
        sender.Send(
            new HowItWorksGetAllRequest(),
            cancellationToken
        ).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow
    ) : IHandler<HowItWorksGetAllRequest, HowItWorksVM>
    {

        public async Task<HowItWorksVM> Handle(
            HowItWorksGetAllRequest request,
            CancellationToken cancellationToken
        )
        {
            var entities = await uow.GetRepository<HowItWorksEntity>().GetAsync();
            var entity = HowItWorksVM.Map(entities.FirstOrDefault()!)
                ?? HowItWorksVM.Empty;

            return entity;
        }
    }
}
