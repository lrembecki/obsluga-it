using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class UserRepository(IUnitOfWork uow) : EfRepository<UserEntity>(uow), IUserRepository
{
    private readonly DbSet<UserEntity> _users = (uow as EfUnitOfWork)!.DbContext.Set<UserEntity>();

    public Task<UserEntity?> GetByEmailAsync(string email)
        => _users
        .Include(e => e.UserSubscriptions)
        .ThenInclude(e => e.Subscription)
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == email);
}