using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Set<User>();

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await dbContext.SaveChangesAsync();
    }

    public Task<User?> GetByEmailAsync(string email)
        => _users
        .Include(e => e.UserSubscriptions)
        .ThenInclude(e => e.Subscription)
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email.Address == email);

    public Task<User?> GetByIdAsync(Guid id)
        => _users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
}