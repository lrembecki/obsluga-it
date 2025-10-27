using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.ToTable(nameof(SubscriptionEntity));

        builder.HasKey(x => x.Id);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Id).HasColumnName("SubscriptionId");
        builder.Property(e => e.Name).HasMaxLength(150).IsRequired();
        builder.Property(e => e.CreatedById);
        builder.Property(e => e.UpdatedById);
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
    }
}