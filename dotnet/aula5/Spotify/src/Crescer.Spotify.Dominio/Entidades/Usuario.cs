namespace Crescer.Spotify.Dominio.Entidades
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(string nome)
        {
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; private set; }        
    }    
}