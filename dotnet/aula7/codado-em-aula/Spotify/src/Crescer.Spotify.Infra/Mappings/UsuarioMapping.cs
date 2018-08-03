using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Spotify.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(20);

            builder.Property(p => p.Login).HasMaxLength(128);

            builder.Property(p => p.Senha).HasMaxLength(128);
        }
    }
}