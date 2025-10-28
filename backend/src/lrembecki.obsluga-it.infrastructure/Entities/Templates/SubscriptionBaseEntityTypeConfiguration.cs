using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities.Templates;

internal class SubscriptionBaseEntityTypeConfiguration<T> : BaseEntityTypeConfiguration<T>
    where T : SubscriptionBaseEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.HasOne(e => e.Subscription)
           .WithMany()
           .HasForeignKey(a => a.SubscriptionId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);
    }
}