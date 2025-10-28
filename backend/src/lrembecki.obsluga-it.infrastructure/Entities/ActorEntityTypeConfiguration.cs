using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class ActorEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<ActorEntity>
{
    public override void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Actor");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Firstname).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Lastname).HasMaxLength(500);

        builder.HasMany(a => a.Contacts)
            .WithOne()
            .HasForeignKey(c => c.ActorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
