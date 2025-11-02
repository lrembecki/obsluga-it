using lrembecki.core.account.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Account;

internal class PermissionEntityTypeConfiguration : BaseEntityTypeConfiguration<PermissionEntity>
{
    public override void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Permission");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(e => new { e.Name }).IsUnique();
    }
}
