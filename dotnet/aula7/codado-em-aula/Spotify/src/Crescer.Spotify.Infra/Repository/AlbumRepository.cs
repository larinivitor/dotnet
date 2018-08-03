using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private SpotifyContext contexto;

        public AlbumRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }

        public void AtualizarAlbum(int id, Album albumAtualizado)
        {
            var album = contexto.Albums.FirstOrDefault(a => a.Id == id);
            album?.Atualizar(albumAtualizado);
        }

        public void DeletarAlbum(int id)
        {
            var album = contexto.Albums.FirstOrDefault(a => a.Id == id);
            contexto.Albums.Remove(album);
        }

        public List<Album> ListarAlbums()
        {
            // O include faz os joins para trazer o objeto carregado
            return contexto.Albums.Include(p => p.Musicas).AsNoTracking().ToList();
        }

        public Album Obter(int id)
        {
            return contexto.Albums.Include(a => a.Musicas).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public double ObterAvaliacao(int id)
        {
            var idMusicasAlbum = contexto.Albums.AsNoTracking().Where(a => a.Id == id)
                                    .SelectMany(m => m.Musicas).Select(m => m.Id);

            var avaliacoesAlbum = contexto.Avaliacoes.AsNoTracking()
                                    .Where(a => idMusicasAlbum.Contains(a.Musica.Id))
                                    .Select(a => a.Nota).ToList();

            return avaliacoesAlbum.Any() ? avaliacoesAlbum.Average() : 0;
        }

        public void SalvarAlbum(Album album)
        {
            contexto.Albums.Add(album);
        }
    }
}