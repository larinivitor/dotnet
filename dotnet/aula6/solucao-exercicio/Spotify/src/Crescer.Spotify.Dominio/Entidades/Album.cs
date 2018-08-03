using System.Collections.Generic;

namespace Crescer.Spotify.Dominio.Entidades
{
    public class Album
    {
        private Album() { }

        public Album(string nome)
        {
            Musicas = new List<Musica>();
            Nome = nome;
        }

        public Album(string nome, List<Musica> musicas)
        {
            Nome = nome;
            Musicas = musicas;
        }

        public int Id { get; private set; }
        
        public string Nome { get; private set; }
        
        public List<Musica> Musicas { get; private set; }
        

        public void Atualizar(Album album)
        {
            Nome = album.Nome;
        }
    }
}