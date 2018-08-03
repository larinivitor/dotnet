using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Api.Models;
using PetStore.Dominio.Contratos;
using PetStore.Dominio.Entidades;
using PetStore.Infra;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private PetStoreContext contexto;

        private IPetRepository petRepository;

        public PetsController(PetStoreContext contexto, IPetRepository petRepository)
        {
            this.contexto = contexto;
            this.petRepository = petRepository;
        }

        [HttpGet]
        public IActionResult BuscarPets()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = "GetPet")]
        public IActionResult BuscarPetPorId(int id)
        {
            var pet = petRepository.ObterPorId(id);

            if (pet == null) return NotFound();

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult CriarPet([FromBody]PetRequestDTO petDto)
        {
            var categoria = petRepository.ObterCategoriaPorId(petDto.IdCategoria);

            if (categoria == null) return BadRequest("A categoria é obrigatoria");

            var pet = new Pet(petDto.Nome, petDto.Status, categoria,
                petDto.Tags.Select(tag => new Tag(tag)).ToList());

            var petCadastrado = petRepository.Cadastrar(pet);

            contexto.SaveChanges();

            return CreatedAtRoute("GetPet", new { id = petCadastrado.Id }, petCadastrado);
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPet(int id, [FromBody]PetRequestDTO petAtualizado)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverPet(int id)
        {
            return Ok();
        }
    }
}