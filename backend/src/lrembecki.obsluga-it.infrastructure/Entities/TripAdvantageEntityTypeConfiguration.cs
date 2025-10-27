using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripAdvantageEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<TripAdvantageEntity>
{
    public override void Configure(EntityTypeBuilder<TripAdvantageEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TripAdvantage");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(500);
    }
}
