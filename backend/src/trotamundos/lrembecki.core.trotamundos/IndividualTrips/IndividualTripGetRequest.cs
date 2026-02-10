using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.IndividualTrips;

public sealed record IndividualTripGetRequest() : IRequest<IndividualTripVM>
{
    public static Delegate Delegate => (ISender sender, CancellationToken ct) => sender.Send(new IndividualTripGetRequest(), ct).ToServiceCallResultAsync();
    internal sealed class Handler(IUnitOfWork uow) : IHandler<IndividualTripGetRequest, IndividualTripVM>
    {
        public async Task<IndividualTripVM> Handle(IndividualTripGetRequest request, CancellationToken ct = default)
        {
            var repository = uow.GetRepository<IndividualTripEntity>();
            var entities = await repository.GetAsync();
            var entity = entities.SingleOrDefault();

            return IndividualTripVM.Map(entity!) ?? IndividualTripVM.Empty();
        }
    }
}
