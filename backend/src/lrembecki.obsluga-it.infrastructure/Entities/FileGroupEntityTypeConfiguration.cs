using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class FileGroupEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<FileGroupEntity>
{
    public override void Configure(EntityTypeBuilder<FileGroupEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("FileGroup");
        builder.HasKey(a => a.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
