using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TagEnityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<TagEntity>
{
    public override void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Tag");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
