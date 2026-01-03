using lrembecki.core.settings.Entities;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class ContactEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<ContactEntity>
{
    public override void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Contact");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Email)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(e => e.Phone)
            .IsRequired(false)
            .HasMaxLength(15);
    }
}
