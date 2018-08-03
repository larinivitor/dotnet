using PetStore.Api.Models;
using System.Collections.Generic;

namespace PetStore.Api.Database
{
    public class UsuarioDatabase
    {
        public int Id { get; set; }

        public List<UsuarioComSenha> Usuarios { get; set; }
    }
}