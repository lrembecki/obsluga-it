using lrembecki.core.settings.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Settings;

internal class EmailEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<EmailEntity>
{
    public override void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.SmtpServer)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.SmtpPort)
            .IsRequired();

        builder.Property(e => e.SmtpUsername)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.SmtpPassword)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.FromAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.FromName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.ReplyToAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.ReplyToName)
            .IsRequired()
            .HasMaxLength(200);
    }
}
