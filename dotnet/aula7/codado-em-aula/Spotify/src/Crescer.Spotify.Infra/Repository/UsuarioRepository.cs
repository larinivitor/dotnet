using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            // Busca o usuario do banco, observando os valores originais
            var usuario = contexto.Usuarios.FirstOrDefault(a => a.Id == id);

            // Edita os campos do usuario (Quando chamar o SaveChanges o EF vai gerar um update)
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
            // Como os valores só serão utilizados para retornar da API, utilizamos o AsNoTracking
            return contexto.Usuarios.AsNoTracking().ToList();
        }

        public Usuario Obter(int id)
        {
            // Como os valores só serão utilizados para retornar da API, utilizamos o AsNoTracking
            return contexto.Usuarios.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Usuario ObterUsuarioPorLoginESenha(string login, string senha)
        {
            var senhaCriptografada = CriptografarSenha(senha);

            return contexto.Usuarios.AsNoTracking()
                .FirstOrDefault(u => u.Login == login && u.Senha == senhaCriptografada);
        }

        public void SalvarUsuario(Usuario usuario)
        {
            usuario.AlterarSenha(CriptografarSenha(usuario.Senha));
            contexto.Usuarios.Add(usuario);
        }

        private string CriptografarSenha(string senha)
        {
            var inputBytes = Encoding.UTF8.GetBytes(senha);

            var hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}