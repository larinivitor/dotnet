using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Spotify.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private SpotifyContext contexto;
        public UsuarioRepository(SpotifyContext contexto)
        {
            this.contexto = contexto;
        }
        public void AtualizarUsuario(int id, Usuario usuarioAtualizado)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(a => a.Id == id);
            usuario?.Atualizar(usuarioAtualizado);
        }

        public void Avaliar(int idUsuario, int idMusica, int nota)
        {
            var avaliacaoParaAMusica = contexto.Avaliacoes.FirstOrDefault(p => p.Musica.Id == idMusica && p.Usuario.Id == idUsuario);

            if (avaliacaoParaAMusica == null)
            {
                var musica = contexto.Musicas.FirstOrDefault(u => u.Id == idMusica);
                var usuario = contexto.Usuarios.FirstOrDefault(u => u.Id == idUsuario);

                contexto.Avaliacoes.Add(new Avaliacao(nota, usuario, musica));
            }
            else
            {
                avaliacaoParaAMusica.AlterarNota(nota);
            }
        }

        public void DeletarUsuario(int id)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(a => a.Id == id);
            contexto.Usuarios.Remove(usuario);
        }

        public List<Usuario> ListarUsuario()
        {
            return contexto.Usuarios.AsNoTracking().ToList();
        }

        public Usuario Obter(int id)
        {
            return contexto.Usuarios.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public void SalvarUsuario(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
        }
    }
}