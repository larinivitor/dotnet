using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Api.Models;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private static int idCorrente = 1;
        
        private static List<Pet> pets = new List<Pet>();

        [HttpGet]
        public IActionResult BuscarPets()
        {
            return Ok(pets);
        }

        [HttpGet("{id}", Name="GetPet")]
        public IActionResult BuscarPetPorId(int id)
        {
            var pet = pets.FirstOrDefault(x => x.Id == id);

            if(pet == null) return NotFound($"O pet id {id} não foi encontrado");

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult CriarPet([FromBody]Pet pet)
        {
            if(pet == null) return BadRequest($"O parametro {nameof(pet)} não pode ser nulo");

            pet.Id = idCorrente++;
            pets.Add(pet);

            return CreatedAtRoute("GetPet", new { id = pet.Id }, pet);
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPet(int id, [FromBody]Pet updatedPet)
        {
            if(updatedPet == null) return BadRequest($"O parametro {nameof(updatedPet)} não pode ser nulo");

            updatedPet.Id = id;

            var pet = pets.FirstOrDefault(x => x.Id == id);

            if(pet == null) return NotFound($"O pet id {id} não foi encontrado");

            pets.Remove(pet);
            pets.Add(updatedPet);

            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverPet(int id)
        {
            var pet = pets.FirstOrDefault(x => x.Id == id);

            if(pet == null) return NotFound($"O pet id {id} não foi encontrado");

            pets.Remove(pet);

            return Ok(pet);
        }
    }
}