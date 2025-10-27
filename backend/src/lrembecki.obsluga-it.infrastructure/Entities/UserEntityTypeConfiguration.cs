using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("UserId");

        builder.Property(e => e.Email)
            .IsRequired().HasMaxLength(150)
            .HasConversion(e => e.Address, e => new Email(e));

        builder.Property(e => e.CreatedById);
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedById);
        builder.Property(e => e.UpdatedAt);
    }
}
