using lrembecki.core.trotamundos.Highlights;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class HighlightEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<HighlightEntity>
{
    public override void Configure(EntityTypeBuilder<HighlightEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);

        builder.ToTable("TrotamundosHighlight");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Icon)
            .IsRequired()
            .HasMaxLength(50);
    }
}
