using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class UserRepository(ApplicationDbContext dbContext) : EfRepository<UserEntity>(dbContext), IUserRepository
{
    private readonly DbSet<UserEntity> _users = dbContext.Set<UserEntity>();

    public Task<UserEntity?> GetByEmailAsync(string email)
        => _users
        .Include(e => e.UserSubscriptions)
        .ThenInclude(e => e.Subscription)
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email.Address == email);
}