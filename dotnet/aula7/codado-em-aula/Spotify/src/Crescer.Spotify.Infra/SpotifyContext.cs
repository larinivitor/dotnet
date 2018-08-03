using System;
using System.Data.SqlClient;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra
{
    public class SpotifyContext : DbContext
    {
        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Musica> Musicas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumMapping());
            modelBuilder.ApplyConfiguration(new MusicaMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new AvaliacaoMapping());
        }
    }
}