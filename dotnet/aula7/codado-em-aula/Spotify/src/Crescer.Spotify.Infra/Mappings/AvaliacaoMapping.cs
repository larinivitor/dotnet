using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Spotify.Infra.Mappings
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacao");

            builder.HasIndex(p => p.Id);

            builder.Property(p => p.Nota);

            builder.HasOne(p => p.Usuario).WithMany().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Musica).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}