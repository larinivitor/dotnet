using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetStore.Api.Controllers;
using PetStore.Api.Models;

namespace PetStore.Tests
{
    [TestClass]
    public class PetsControllerTests
    {
        [TestMethod]
        public void Apos_A_Criacao_De_Um_Pet_Deve_Ser_Possivel_Pesquisar_Pelo_Id_Gerado_Para_O_Mesmo()
        {
            var controller = new PetsController();

            var resultadoCriacao = controller.CriarPet(new Pet()
            {
                Nome = "Teddy",
                Status = StatusPet.Disponivel,
                Categoria = new Categoria
                {
                    Nome = "Basset-Hound"
                },
                Tags = new List<Tag> { new Tag { Descricao = "Babão" }, new Tag { Descricao = "Brincalhão" } }
            });

            var criado = resultadoCriacao as CreatedAtRouteResult;

            Assert.IsNotNull(criado);

            var petCriado = criado.Value as Pet;

            Assert.IsNotNull(petCriado);

            var resultadoBusca = controller.BuscarPetPorId(petCriado.Id);

            Assert.IsNotNull(resultadoBusca as OkObjectResult);
        }

        [TestMethod]
        public void Pesquisar_Um_Pet_Por_Um_Id_Inexistente_Deve_Retornar_Not_Found()
        {
            var controller = new PetsController();

            var resultadoBusca = controller.BuscarPetPorId(int.MaxValue);

            Assert.IsNotNull(resultadoBusca as NotFoundObjectResult);
        }
    }
}