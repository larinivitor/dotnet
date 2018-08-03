using System;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Avaliacao
    {
        private Avaliacao() { }

        public Avaliacao(int nota, Usuario usuario, Musica musica)
        {
            this.Nota = nota;
            this.Usuario = usuario;
            this.Musica = musica;
        }

        public int Id { get; private set; }

        public int Nota { get; private set; }

        public Usuario Usuario { get; private set; }

        public Musica Musica { get; private set; }

        public void AlterarNota(int nota)
        {
            Nota = nota;
        }
    }
}