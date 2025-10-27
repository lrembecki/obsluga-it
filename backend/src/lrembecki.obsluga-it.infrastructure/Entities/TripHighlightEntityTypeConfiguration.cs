using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripHighlightEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<TripHighlightEntity>
{
    public override void Configure(EntityTypeBuilder<TripHighlightEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);

        builder.ToTable("TripHighlight");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Icon)
            .IsRequired()
            .HasMaxLength(50);
    }
}
