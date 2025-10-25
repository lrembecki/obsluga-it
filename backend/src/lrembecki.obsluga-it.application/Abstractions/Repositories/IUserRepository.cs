using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface IUserRepository : IRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}
