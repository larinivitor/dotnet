using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IUsuarioRepository
    {
        void SalvarUsuario(Usuario usuario);
        void AtualizarUsuario(int id, Usuario usuario);
        void DeletarUsuario(int id);
        void Avaliar(int idUsuario, int idMusica, int nota);
        List<Usuario> ListarUsuario();
        Usuario Obter(int id);
        Usuario ObterUsuarioPorLoginESenha(string login, string senha);
    }
}