using lrembecki.obsluga_it.domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities.Templates;

internal class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.HasIndex(e => e.UpdatedAt).IsDescending(true);
    }
}