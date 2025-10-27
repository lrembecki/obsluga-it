using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class UnitOfMeasureEntityTypeConfiguration : BaseEntityTypeConfiguration<UnitOfMeasureEntity>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasureEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("UnitOfMeasure");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ShortName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ShortCode)
            .IsRequired()
            .HasMaxLength(50);
    }
}