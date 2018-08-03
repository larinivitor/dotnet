using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {
        private SpotifyContext contexto;

        public MusicaRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }
        public void AtualizarMusica(int id, Musica musicaAtualizado)
        {
            var musica = contexto.Musicas.FirstOrDefault(a => a.Id == id);
            musica?.Atualizar(musicaAtualizado);
        }

        public void DeletarMusica(int id)
        {
            var musica = contexto.Musicas.FirstOrDefault(a => a.Id == id);
            contexto.Musicas.Remove(musica);
        }

        public List<Musica> ListarMusicas(int idAlbum)
        {
            return contexto.Albums.Include(m => m.Musicas).AsNoTracking().FirstOrDefault(a => a.Id == idAlbum).Musicas;
        }

        public Musica Obter(int id)
        {
            return contexto.Musicas.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public double ObterAvaliacao(int id)
        {
            var notas = contexto.Avaliacoes.AsNoTracking().Where(a => a.Musica.Id == id).Select(a => a.Nota).ToList();

            return notas.Any() ? notas.Average() : 0;
        }

        public void SalvarMusica(int idAlbum, Musica musica)
        {
            var album = contexto.Albums.Include(a => a.Musicas).FirstOrDefault(a => a.Id == idAlbum);
            album.Musicas.Add(musica);
        }
    }
}