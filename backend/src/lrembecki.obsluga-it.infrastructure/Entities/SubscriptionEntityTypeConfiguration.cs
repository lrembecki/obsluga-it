using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class SubscriptionEntityTypeConfiguration : BaseEntityTypeConfiguration<SubscriptionEntity>
{
    public override void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Subscription");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name).HasMaxLength(150).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique();
    }
}