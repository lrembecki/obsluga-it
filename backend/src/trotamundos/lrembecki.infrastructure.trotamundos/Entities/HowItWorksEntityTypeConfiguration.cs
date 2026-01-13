using lrembecki.core.trotamundos.Pages.HowItWorks;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class HowItWorksEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<HowItWorksEntity>
{
    public override void Configure(EntityTypeBuilder<HowItWorksEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosPagesHowItWorks");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.HeaderText)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.FooterText)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(e => e.Items).WithOne()
            .HasForeignKey(e => e.HowItWorksId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Items).AutoInclude();
    }
}
