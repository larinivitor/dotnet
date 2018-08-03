using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetStore.Api.Database;
using PetStore.Api.Models;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private static readonly string DatabasePath = $"{Path.GetTempPath()}pet-database.json";

        public PetsController()
        {
            if (!System.IO.File.Exists(DatabasePath))
                System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(new PetDatabase()
                {
                    Id = 1,
                    Pets = new List<Pet>()
                }));
        }

        [HttpGet]
        public IActionResult BuscarPets()
        {
            var database = CarregarDatabase();

            return Ok(database.Pets);
        }

        [HttpGet("{id}", Name = "GetPet")]
        public IActionResult BuscarPetPorId(int id)
        {
            var database = CarregarDatabase();

            var pet = database.Pets.FirstOrDefault(x => x.Id == id);

            if (pet == null) return NotFound($"O pet id {id} não foi encontrado");

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult CriarPet([FromBody]Pet pet)
        {
            var database = CarregarDatabase();

            if (pet == null) return BadRequest($"O parametro {nameof(pet)} não pode ser nulo");

            if (pet.Tags == null) return BadRequest($"O parametro {nameof(pet.Tags)} não pode ser nulo");

            pet.Id = database.Id++;
            database.Pets.Add(pet);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return CreatedAtRoute("GetPet", new { id = pet.Id }, pet);
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPet(int id, [FromBody]Pet petAtualizado)
        {
            var database = CarregarDatabase();

            if (petAtualizado == null) return BadRequest($"O parametro {nameof(petAtualizado)} não pode ser nulo");

            petAtualizado.Id = id;

            var pet = database.Pets.FirstOrDefault(x => x.Id == id);

            if (pet == null) return NotFound($"O pet id {id} não foi encontrado");

            database.Pets.Remove(pet);
            database.Pets.Add(petAtualizado);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return Ok(petAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverPet(int id)
        {
            var database = CarregarDatabase();

            var pet = database.Pets.FirstOrDefault(x => x.Id == id);

            if (pet == null) return NotFound($"O pet id {id} não foi encontrado");

            database.Pets.Remove(pet);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return Ok(pet);
        }

        [HttpGet("{id}/tags", Name = "GetPetTags")]
        public IActionResult BuscarTagsDoPet(int id)
        {
            var database = CarregarDatabase();

            var pet = database.Pets.FirstOrDefault(x => x.Id == id);

            if (pet == null) return NotFound($"O pet id {id} não foi encontrado");

            return Ok(pet.Tags);
        }

        [HttpPost("{id}/tags")]
        public IActionResult AdicionarTagAoPet(int id, [FromBody]Tag tag)
        {
            var database = CarregarDatabase();

            var pet = database.Pets.FirstOrDefault(x => x.Id == id);

            if (pet == null) return NotFound($"O pet id {id} não foi encontrado");

            pet.Tags.Add(tag);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return CreatedAtRoute("GetPetTags", new { id = pet.Id }, tag);
        }

        [HttpGet("tags")]
        public IActionResult BuscarTagsDosPets()
        {
            var database = CarregarDatabase();

            var tags = database.Pets.SelectMany(pet => pet.Tags).ToList();

            return Ok(tags);
        }

        private PetDatabase CarregarDatabase()
        {
            return JsonConvert.DeserializeObject<PetDatabase>(System.IO.File.ReadAllText(DatabasePath));
        }
    }
}