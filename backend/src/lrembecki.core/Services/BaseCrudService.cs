using lrembecki.core.Helpers;
using lrembecki.core.Markers;

using System.Linq.Expressions;
using System.Reflection;

namespace lrembecki.core.Services
{
#pragma warning disable S2436 // Types and methods should not have too many generic parameters
    public class BaseCrudService<TEntity, TVM, TDto>(IUnitOfWork uow) : ICrudService<TDto, TVM>
#pragma warning restore S2436 // Types and methods should not have too many genericParameters
        where TEntity : class, IHasId<Guid>
        where TVM : class
        where TDto : class
    {
        protected readonly IUnitOfWork _uow = uow;
        protected readonly IRepository<TEntity> _repository = uow.GetRepository<TEntity>();
        public async Task<TVM> CreateAsync(TDto model, CancellationToken cancellationToken = default)
        {
            model = await Validate(model);

            var entity = await CreateEntity(Guid.NewGuid(), model, cancellationToken);
            await _repository.AddAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(entity.Id, cancellationToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await DeleteEntity(await _repository.RequireByIdAsync(id, cancellationToken), cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }
        public virtual Task<List<TVM>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = GetAll(_repository.GetAll());
            return Task.FromResult(query.Select(BuildSelectExpression()).ToList());
        }

        protected virtual IQueryable<TEntity> GetAll(IQueryable<TEntity> query) => query;

        private MethodInfo GetMapMethod()
        {
            var vmType = typeof(TVM);
            var entityType = typeof(TEntity);
            
            var mapMethod = vmType.GetMethod("Map", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, [entityType]);

            if (mapMethod == null)
            {
                foreach (var m in vmType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
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
                throw new InvalidOperationException($"Type {vmType.Name} must declare a public or internal static Map method that accepts {entityType.Name}.");

            return mapMethod;
        }

        public async virtual Task<TVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => (TVM)GetMapMethod().Invoke(null, new object[] { (await _repository.FindByIdAsync(id, cancellationToken))! })!;
        public async Task<TVM> UpdateAsync(Guid id, TDto model, CancellationToken cancellationToken = default)
        {
            model = await Validate(model);

            var entity = await _repository.RequireByIdAsync(id, cancellationToken);

            await UpdateEntity(entity, model);

            await _repository.UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(entity.Id, cancellationToken);
        }

        private Expression<Func<TEntity, TVM>> BuildSelectExpression()
        {
            var vmType = typeof(TVM);
            var entityType = typeof(TEntity);
            var mapMethod = GetMapMethod();

            var param = Expression.Parameter(entityType, "e");
            var methodParamType = mapMethod.GetParameters()[0].ParameterType;

            Expression arg = param;
            if (methodParamType != entityType)
                arg = Expression.Convert(param, methodParamType);

            var call = Expression.Call(mapMethod, arg);

            return Expression.Lambda<Func<TEntity, TVM>>(call, param);
        }

        protected virtual Task<TEntity> CreateEntity(Guid id, TDto model, CancellationToken cancellationToken)
        {
            var entity = (TEntity)typeof(TEntity).GetMethod("Create")!.Invoke(null, new object[] { id, model })!;

            return Task.FromResult(entity);
        }
        protected virtual Task UpdateEntity(TEntity entity, TDto model)
        {
            typeof(TEntity).GetMethod("Update")!.Invoke(entity, new object[] { model });

            return Task.CompletedTask;
        }
        protected virtual async Task DeleteEntity(TEntity entity, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);
        }
        protected virtual Task<TDto> Validate(TDto model) => Task.FromResult(model);
    }
}
