using lrembecki.core.trotamundos.Pages.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class HomeOpinionEntityTypeConfiguration : IEntityTypeConfiguration<HomeOpinionEntity>
{
    public void Configure(EntityTypeBuilder<HomeOpinionEntity> builder)
    {
        builder.HasKey(e => new { e.HomeId, e.Order });
        builder.ToTable("TrotamundosPagesHomeOpinion");

        builder.HasOne(e => e.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
