using lrembecki.core.account.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Account;

internal class PermissionGroupEntityTypeConfiguration : BaseEntityTypeConfiguration<PermissionGroupEntity>
{
    public override void Configure(EntityTypeBuilder<PermissionGroupEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("PermissionGroup");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(e => new { e.Name }).IsUnique();

        builder.HasMany(e => e.Permissions).WithMany().UsingEntity<Dictionary<string, string>>("PermissionGroupPermission",
            j => j.HasOne<PermissionEntity>().WithMany().HasForeignKey("PermissionId").OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<PermissionGroupEntity>().WithMany().HasForeignKey("PermissionGrupId").OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.ToTable("PermissionGroupPermission");
                j.HasKey("PermissionGrupId", "PermissionId");
            });
    }
}