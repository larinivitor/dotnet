using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public void AtualizarAlbum(int id, Album album)
        {
            var albumObtido = Obter(id);
            albumObtido?.Atualizar(album);            
        }

        public void DeletarAlbum(int id)
        {
            var album = this.Obter(id);
            Repositorio.albuns.Remove(album);
        }

        public List<Album> ListarAlbum()
        {
            return Repositorio.albuns;
        }

        public Album Obter(int id)
        {
            return Repositorio.albuns.Where(x => x.Id == id).FirstOrDefault();
        }

        public void SalvarAlbum(Album album)
        {
            album.Id = Repositorio.idAlbum++;
            Repositorio.albuns.Add(album);            
        }     
    }
}