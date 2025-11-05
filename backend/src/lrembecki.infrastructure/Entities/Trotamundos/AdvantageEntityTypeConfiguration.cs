using lrembecki.core.trotamundos.Entitites;
using lrembecki.infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

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
