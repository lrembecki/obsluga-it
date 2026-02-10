using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.IndividualTrips;

internal class IndividualTripEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public List<IndividualTripItemEntity> Items { get; private set; } = [];
    public List<IndividualTripStepItemEntity> Steps { get; private set; } = [];

        public static IndividualTripEntity Create(
            Guid id,
            IndividualTripDto model
        )
        {
            var entity = new IndividualTripEntity
            {
                Id = id
            };
    
            entity.Update(model);
    
            return entity;
    }

    public void Update(IndividualTripDto model)
    {
        Title = model.Title;
        Description = model.Description;

        // Update Items
        Items.Clear();
        Items.AddRange(model.Items.Select(item => IndividualTripItemEntity.Create(Id, item)));

        // Update Steps
        var toDelete = Steps.Where(e => !model.Steps.Select(y => y.Order).Contains(e.Order)).ToList();
        var toUpdate = Steps.Where(e => model.Steps.Select(y => y.Order).Contains(e.Order)).ToList();
        var toCreate = model.Steps.Where(e => !Steps.Select(y => y.Order).Contains(e.Order)).ToList();
        foreach (var item in toUpdate) item.Update(model.Steps.Find(y => y.Order == item.Order)!);
        foreach (var item in toDelete) Steps.Remove(item);
        foreach (var item in toCreate) Steps.Add(IndividualTripStepItemEntity.Create(Id, item));
    }
}
