using lrembecki.core.Markers;

using System.Linq.Expressions;
using System.Reflection;

namespace lrembecki.core.Services;
#pragma warning disable S2436 // Types and methods should not have too many generic parameters
public class BaseCrudService<TEntity, TVM, TDto>(IUnitOfWork uow) : ICrudService<TDto, TVM>
#pragma warning restore S2436 // Types and methods should not have too many genericParameters
    where TEntity : class, IHasId<Guid>
    where TVM : class
    where TDto : class
{
    protected readonly IRepository<TEntity> _repository = uow.GetRepository<TEntity>();
    public async Task<TVM> CreateAsync(TDto model, CancellationToken cancellationToken = default)
    {
        var entity = await CreateEntity(model, cancellationToken);

        await UpdateEntity(entity, model);
        await _repository.AddAsync(entity);

        return await GetByIdAsync(entity.Id, cancellationToken);
    }
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        => await _repository.DeleteAsync(await _repository.RequireByIdAsync(id, cancellationToken));
    public async virtual Task<List<TVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _repository.SelectAsync(BuildSelectExpression());
    public async virtual Task<TVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => (TVM)typeof(TVM).GetMethod("Map", BindingFlags.Public | BindingFlags.Static)!.Invoke(null, new object[] { await _repository.RequireByIdAsync(id, cancellationToken) })!;
    public async Task<TVM> UpdateAsync(Guid id, TDto model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.RequireByIdAsync(id, cancellationToken);

        await UpdateEntity(entity, model);

        await _repository.UpdateAsync(entity);

        return await GetByIdAsync(entity.Id, cancellationToken);
    }

    private Expression<Func<TEntity, TVM>> BuildSelectExpression()
    {
        var vmType = typeof(TVM);
        var entityType = typeof(TEntity);
        // Try to find Map(TEntity) first
        var mapMethod = vmType.GetMethod("Map", BindingFlags.Static | BindingFlags.Public, [entityType]);

        // Fallback: find any public static Map method whose single parameter can accept TEntity
        if (mapMethod == null)
        {
            foreach (var m in vmType.GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                if (m.Name != "Map")
                    continue;

                var parameters = m.GetParameters();
                if (parameters.Length != 1)
                    continue;

                var paramType = parameters[0].ParameterType;
                if (paramType.IsAssignableFrom(entityType))
                {
                    mapMethod = m;
                    break;
                }
            }
        }

        if (mapMethod == null)
            throw new InvalidOperationException($"Type {vmType.Name} must declare a public static Map method that accepts {entityType.Name}.");

        var param = Expression.Parameter(entityType, "e");
        var methodParamType = mapMethod.GetParameters()[0].ParameterType;

        Expression arg = param;
        if (methodParamType != entityType)
            arg = Expression.Convert(param, methodParamType);

        var call = Expression.Call(mapMethod, arg);

        return Expression.Lambda<Func<TEntity, TVM>>(call, param);
    }

    protected virtual Task<TEntity> CreateEntity(TDto model, CancellationToken cancellationToken)
    {
        var entity = (TEntity)typeof(TEntity).GetMethod("Create")!.Invoke(null, new object[] { Guid.NewGuid(), model })!;

        return Task.FromResult(entity);
    }

    protected virtual Task UpdateEntity(TEntity entity, TDto model)
    {
        typeof(TEntity).GetMethod("Update")!.Invoke(entity, new object[] { model });

        return Task.CompletedTask;
    }
}
