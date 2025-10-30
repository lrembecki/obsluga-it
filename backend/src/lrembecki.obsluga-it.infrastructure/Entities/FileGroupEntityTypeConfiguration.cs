using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using lrembecki.shared.domain.Entities;
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
