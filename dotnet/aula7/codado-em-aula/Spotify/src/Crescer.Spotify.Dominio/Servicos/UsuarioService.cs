using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class UsuarioService
    {
        public List<string> Validar(Usuario usuario)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(usuario.Nome?.Trim()))
                mensagens.Add("É necessário informar o nome do usuário.");
            
            return mensagens;
        }
    }
}