using System.Collections.Generic;

namespace PetStore.Api.Models
{
    public class Pet
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Categoria Categoria { get; set; }

        public List<Tag> Tags { get; set; }

        public StatusPet Status { get; set; } 
    }
}