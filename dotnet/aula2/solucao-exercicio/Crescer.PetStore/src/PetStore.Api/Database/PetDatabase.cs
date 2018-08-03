using PetStore.Api.Models;
using System.Collections.Generic;

namespace PetStore.Api.Database
{
    public class PetDatabase
    {
        public int Id { get; set; }

        public List<Pet> Pets { get; set; }
    }
}