using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("UserId");

        builder.Property(e => e.Email)
            .IsRequired().HasMaxLength(150);

        builder.Property(e => e.CreatedById);
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedById);
        builder.Property(e => e.UpdatedAt);
    }
}
