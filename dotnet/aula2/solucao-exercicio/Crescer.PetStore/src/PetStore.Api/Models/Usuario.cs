namespace PetStore.Api.Models
{
    public class Usuario
    {
        public Usuario() { }

        public Usuario(UsuarioComSenha usuarioComSenha)
        {
            Id = usuarioComSenha.Id;
            Login = usuarioComSenha.Login;
            PrimeiroNome = usuarioComSenha.PrimeiroNome;
            UltimoNome = usuarioComSenha.UltimoNome;
            Email = usuarioComSenha.Email;
            Telefone = usuarioComSenha.Telefone;
            StatusUsuario = usuarioComSenha.StatusUsuario;
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public StatusUsuario StatusUsuario { get; set; }
    }
}