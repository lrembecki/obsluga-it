using lrembecki.core.settings.Address;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class AddressEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<AddressEntity>
{
    public override void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("Address");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Address1)
            .IsRequired()
            .HasMaxLength(400);
        builder.Property(e => e.Address2)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Country)
            .IsRequired()
            .HasMaxLength(100);
    }
}
