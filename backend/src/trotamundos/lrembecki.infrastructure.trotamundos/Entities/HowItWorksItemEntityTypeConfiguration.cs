using lrembecki.core.trotamundos.Pages.HowItWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class HowItWorksItemEntityTypeConfiguration : IEntityTypeConfiguration<HowItWorksItemEntity>
{
    public void Configure(EntityTypeBuilder<HowItWorksItemEntity> builder)
    {
        builder.HasKey(e => new { e.HowItWorksId, e.Order });
        builder.ToTable("TrotamundosPagesHowItWorksItem");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.HasOne(e => e.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
