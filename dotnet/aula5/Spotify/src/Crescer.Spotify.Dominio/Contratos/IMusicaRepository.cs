using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IMusicaRepository
    {
        void SalvarMusica(int idAlbum, Musica musica);
        void AtualizarMusica(int id, Musica musica);
        void DeletarMusica(int id);
        double ObterAvaliavao(int id);
        List<Musica> ListarMusicas(int idAlbum);             
        Musica Obter(int id);
    }
}