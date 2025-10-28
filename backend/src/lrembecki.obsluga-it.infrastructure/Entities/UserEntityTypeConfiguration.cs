using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("User");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired().HasMaxLength(150);

        builder.HasIndex(e => e.Email).IsUnique();

        builder.HasMany(e => e.UserSubscriptions).WithOne(e => e.User).HasForeignKey(e => e.UserId);
    }
}
