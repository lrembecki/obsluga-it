using lrembecki.core.trotamundos.Entitites;
using lrembecki.infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class HighlightEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<HighlightEntity>
{
    public override void Configure(EntityTypeBuilder<HighlightEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);

        builder.ToTable("Highlight");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Icon)
            .IsRequired()
            .HasMaxLength(50);
    }
}
