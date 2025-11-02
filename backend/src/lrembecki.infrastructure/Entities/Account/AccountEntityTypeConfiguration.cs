using lrembecki.core.account.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Account;

internal class AccountEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountEntity>
{
    public override void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Account");

        builder.Property(e => e.Email).HasMaxLength(250);

        builder.HasKey(e => e.Id);
        builder.HasIndex(e => new { e.Email }).IsUnique();

        builder.HasMany(e => e.PermissionGroups).WithMany().UsingEntity<Dictionary<string, string>>("AccountPermissionGroup",
            j => j.HasOne<PermissionGroupEntity>().WithMany().HasForeignKey("PermissionGroupId").OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<AccountEntity>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.ToTable("AccountPermissionGroup");
                j.HasKey("AccountId", "PermissionGroupId");
            });
    }
}
