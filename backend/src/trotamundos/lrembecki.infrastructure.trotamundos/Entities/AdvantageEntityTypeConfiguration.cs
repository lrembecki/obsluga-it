using lrembecki.core.trotamundos.Advantages;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class AdvantageEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<AdvantageEntity>
{
    public override void Configure(EntityTypeBuilder<AdvantageEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosAdvantage");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(500);
    }
}
