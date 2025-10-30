using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities.Templates;

internal class BlobBaseEntityTypeConfiguration<T> : SubscriptionBaseEntityTypeConfiguration<T>
    where T : BlobBaseEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Filename)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(e => e.BlobUrl)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(e => e.BlobPath)
            .IsRequired(false)
            .HasMaxLength(500);
    }
}