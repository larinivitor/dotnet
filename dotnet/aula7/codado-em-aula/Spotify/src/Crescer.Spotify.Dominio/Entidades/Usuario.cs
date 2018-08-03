using System;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Usuario
    {
        private Usuario() { }

        public Usuario(string nome, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }
        public int Id { get; private set; }

        public string Nome { get; private set; }

        public string Login { get; private set; }

        public string Senha { get; private set; }

        public void Atualizar(Usuario usuarioAtualizado)
        {
            Nome = usuarioAtualizado.Nome;
        }

        public void AlterarSenha(string senha)
        {
            Senha = senha;
        }
    }
}