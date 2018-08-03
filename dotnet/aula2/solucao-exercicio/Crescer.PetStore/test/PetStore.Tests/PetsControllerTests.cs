using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetStore.Api.Controllers;
using PetStore.Api.Models;

namespace PetStore.Tests
{
    [TestClass]
    public class PetsControllerTests
    {
        [TestInitialize]
        public void Startup()
        {
            var databasePath = $"{Path.GetTempPath()}pet-database.json";

            if (File.Exists(databasePath)) File.Delete(databasePath);
        }

        [TestMethod]
        public void Pets_Cadastrados_Devem_Ser_Retornados_No_Obter_Por_Id()
        {
            var controller = new PetsController();

            var resultadoCriacao = controller.CriarPet(CriarTeddy()) as CreatedAtRouteResult;

            Assert.IsNotNull(resultadoCriacao);

            var petCriado = resultadoCriacao.Value as Pet;

            Assert.IsNotNull(petCriado);

            var resultadoBusca = controller.BuscarPetPorId(petCriado.Id);

            Assert.IsNotNull(resultadoBusca as OkObjectResult);
        }

        [TestMethod]
        public void Pets_Atualizados_Devem_Ser_Retornados_No_Obter_Por_Id()
        {
            var pet = CriarTeddy();

            var controller = new PetsController();

            var resultadoCriacao = controller.CriarPet(pet) as CreatedAtRouteResult;

            Assert.IsNotNull(resultadoCriacao);

            var petCriado = resultadoCriacao.Value as Pet;

            petCriado.Nome = "Joaquim";
            var resultadoAlteracao = controller.AlterarPet(petCriado.Id, petCriado) as OkObjectResult;

            Assert.IsNotNull(resultadoAlteracao);

            var resultadoBusca = controller.BuscarPetPorId(petCriado.Id) as OkObjectResult;

            Assert.IsNotNull(resultadoBusca);

            var petBuscado = resultadoBusca.Value as Pet;

            Assert.IsNotNull(petBuscado);

            Assert.AreEqual("Joaquim", petBuscado.Nome);
        }

        [TestMethod]
        public void Pets_Removidos_Nao_Devem_Ser_Retornados_No_Obter_Por_Id()
        {
            var pet = CriarTeddy();

            var petController = new PetsController();

            var resultadoCriacao = petController.CriarPet(pet) as CreatedAtRouteResult;

            Assert.IsNotNull(resultadoCriacao);

            var petCriado = resultadoCriacao.Value as Pet;

            var petRemovido = petController.RemoverPet(petCriado.Id);

            var petRetornadoNoGet = petController.BuscarPetPorId(petCriado.Id) as NotFoundObjectResult;

            Assert.IsNotNull(petRetornadoNoGet);
        }

        [TestMethod]
        public void Todos_Pets_Cadastrados_Devem_Ser_Retornados_No_Obter_Todos()
        {
            var teddy = CriarTeddy();
            var joazinho = CriarJoaozinho();

            var petController = new PetsController();

            var resultadoCriacaoTeddy = petController.CriarPet(teddy);

            var resultadoCriacaoJoazinho = petController.CriarPet(joazinho);

            var resultadoBuscaPets = petController.BuscarPets() as OkObjectResult;

            var pets = resultadoBuscaPets.Value as List<Pet>;

            Assert.AreEqual(2, pets.Count);
        }

        [TestMethod]
        public void Atualizar_Pet_Deve_Retornar_Erro_Quando_O_Pet_For_Nulo()
        {
            var controller = new PetsController();

            var badRequest = controller.AlterarPet(1, null) as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O parametro petAtualizado não pode ser nulo", badRequest.Value);
        }

        [TestMethod]
        public void Atualizar_Pet_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = new PetsController();

            var badRequest = controller.AlterarPet(1, new Pet()) as NotFoundObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O pet id 1 não foi encontrado", badRequest.Value);
        }

        [TestMethod]
        public void Remover_Pet_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = new PetsController();

            var badRequest = controller.RemoverPet(1) as NotFoundObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O pet id 1 não foi encontrado", badRequest.Value);
        }

        [TestMethod]
        public void Pesquisar_Um_Pet_Por_Um_Id_Inexistente_Deve_Retornar_Not_Found()
        {
            var controller = new PetsController();

            var resultadoBusca = controller.BuscarPetPorId(int.MaxValue);

            Assert.IsNotNull(resultadoBusca as NotFoundObjectResult);
        }

        private Pet CriarTeddy()
        {
            return new Pet()
            {
                Nome = "Teddy",
                Status = StatusPet.Disponivel,
                Categoria = new Categoria
                {
                    Nome = "Basset-Hound"
                },
                Tags = new List<Tag> { new Tag { Descricao = "Babão" }, new Tag { Descricao = "Brincalhão" } }
            };
        }

        private Pet CriarJoaozinho()
        {
            return new Pet()
            {
                Nome = "Joaozinho",
                Status = StatusPet.Disponivel,
                Categoria = new Categoria
                {
                    Nome = "Fox"
                },
                Tags = new List<Tag> { new Tag { Descricao = "Frenetico" } }
            };
        }
    }
}