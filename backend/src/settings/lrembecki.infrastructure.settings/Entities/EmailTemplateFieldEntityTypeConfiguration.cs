using lrembecki.core.settings.EmailTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class EmailTemplateFieldEntityTypeConfiguration : IEntityTypeConfiguration<EmailTemplateFieldEntity>
{
    public void Configure(EntityTypeBuilder<EmailTemplateFieldEntity> builder)
    {
        builder.ToTable("EmailTemplateField");
        builder.HasKey(e => new { e.EmailTemplateId, e.FieldName });
    
        builder.Property(e => e.FieldName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne<EmailTemplateEntity>()
            .WithMany(e => e.Fields)
            .HasForeignKey(e => e.EmailTemplateId);
    }
}   