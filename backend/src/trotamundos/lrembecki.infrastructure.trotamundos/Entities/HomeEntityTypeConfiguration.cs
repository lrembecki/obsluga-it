using lrembecki.core.trotamundos.Pages.Home;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class HomeEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<HomeEntity>
{
    public override void Configure(EntityTypeBuilder<HomeEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("TrotamundosPagesHome");
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Opinions).WithOne()
            .HasForeignKey(e => e.HomeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.TrailerVideo)
            .WithMany()
            .HasForeignKey(e => e.TrailerVideoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BackgroundVideo)
            .WithMany()
            .HasForeignKey(e => e.BackgroundVideoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Opinions).AutoInclude();
        builder.Navigation(e => e.TrailerVideo).AutoInclude();
        builder.Navigation(e => e.BackgroundVideo).AutoInclude();
    }
}
