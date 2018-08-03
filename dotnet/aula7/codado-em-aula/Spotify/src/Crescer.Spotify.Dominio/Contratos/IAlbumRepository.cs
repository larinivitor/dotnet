using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IAlbumRepository
    {       
        void SalvarAlbum(Album album);
        
        void AtualizarAlbum(int id, Album album);
        
        void DeletarAlbum(int id);
        
        double ObterAvaliacao(int id);
        
        List<Album> ListarAlbums();
        
        Album Obter(int id);
    }
}