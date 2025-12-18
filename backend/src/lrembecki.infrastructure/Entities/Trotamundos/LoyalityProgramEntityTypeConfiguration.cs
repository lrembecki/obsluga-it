using lrembecki.core.trotamundos.Entitites;
using lrembecki.infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class LoyalityProgramEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<LoyalityProgramEntity>
{
    public override void Configure(EntityTypeBuilder<LoyalityProgramEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosLoyalityProgram");
        builder.HasKey(a => a.Id);

        builder.Property(e => e.Name)
            .IsRequired().HasMaxLength(200);

        builder.Property(e => e.Title)
            .IsRequired().HasMaxLength(500);

        builder.Property(e => e.Description)
            .IsRequired().HasMaxLength(2048);

        builder.HasOne(e => e.Image).WithOne().HasForeignKey<LoyalityProgramEntity>(e => e.ImageId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
