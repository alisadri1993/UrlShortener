using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shortener.Endpoint.Entities.TypeConfigurations
{
    public class ShortLinkConfiguration : IEntityTypeConfiguration<ShortLink>
    {
        public void Configure(EntityTypeBuilder<ShortLink> builder)
        {

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Url).HasMaxLength(256);
            builder.Property(s => s.Token).HasMaxLength(64);

            builder.HasIndex(s => s.Token).IsUnique();
        }
    }
}
