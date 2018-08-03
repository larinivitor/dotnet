using System;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Usuario
    {
        private Usuario() { }

        public Usuario(string nome)
        {
            this.Nome = nome;
        }
        public int Id { get; private set; }

        public string Nome { get; private set; }

        public void Atualizar(Usuario usuarioAtualizado)
        {
            Nome = usuarioAtualizado.Nome;
        }
    }    
}