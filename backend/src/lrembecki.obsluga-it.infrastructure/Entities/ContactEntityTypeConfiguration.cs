using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class ContactEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<ContactEntity>
{
    public override void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Contact");
        builder.HasKey(a => a.Id);

        builder.Property(e => e.Email)
            .HasConversion(
                v => v.Address,
                v => new Email(v))
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(e => e.Phone)
            .HasConversion(
                v => v.Number,
                v => new Phone(v))
            .IsRequired()
            .HasMaxLength(15);

        builder.HasIndex(e => new { e.SubscriptionId, e.Email }).IsUnique();

        builder.HasOne<ActorEntity>().WithMany().HasForeignKey(e => e.ActorId).IsRequired();
    }
}
