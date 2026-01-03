using lrembecki.core.settings.Companies;
using lrembecki.core.settings.Contacts;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class CompanyEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<CompanyEntity>
{
    public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("Company");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(e => e.Address)
            .WithMany()
            .HasForeignKey(e => e.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(e => e.Parameters, paramBuilder =>
        {
            paramBuilder.ToTable("CompanyParameter");
            paramBuilder.HasKey(e => new { e.CompanyId, e.Order });
            paramBuilder.WithOwner().HasForeignKey(e => e.CompanyId);

            paramBuilder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            paramBuilder.Property(e => e.Value).IsRequired(false).HasMaxLength(200);
        });

        builder.HasMany(e => e.EmailContact).WithMany().UsingEntity<Dictionary<string, object>>(
            "CompanyEmailContact",
            e => e.HasOne<ContactEntity>().WithMany().HasForeignKey("ContactId").OnDelete(DeleteBehavior.Restrict),
            e => e.HasOne<CompanyEntity>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.Restrict));

        builder.HasMany(e => e.PhoneContact).WithMany().UsingEntity<Dictionary<string, object>>(
            "CompanyPhoneContact",
            e => e.HasOne<ContactEntity>().WithMany().HasForeignKey("ContactId").OnDelete(DeleteBehavior.Restrict),
            e => e.HasOne<CompanyEntity>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.Restrict));

        builder.Navigation(e => e.Address).AutoInclude();
        builder.Navigation(e => e.Parameters).AutoInclude();
        builder.Navigation(e => e.EmailContact).AutoInclude();
        builder.Navigation(e => e.PhoneContact).AutoInclude();
    }
}