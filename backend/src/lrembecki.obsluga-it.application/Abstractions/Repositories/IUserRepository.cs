using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

internal interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email);
}
