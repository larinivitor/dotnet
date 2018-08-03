using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetStore.Api.Models;
using UsuarioStore.Api.Controllers;

namespace UsuarioStore.Tests
{
    [TestClass]
    public class UsuariosControllerTests
    {
        [TestInitialize]
        public void Startup()
        {
            var databasePath = $"{Path.GetTempPath()}usuario-database.json";

            if (File.Exists(databasePath)) File.Delete(databasePath);
        }

        [TestMethod]
        public void Usuarios_Cadastrados_Devem_Ser_Retornados_No_Obter_Por_Login()
        {
            var controller = new UsuariosController();

            var resultadoCriacao = controller.CriarUsuario(CriarFulaninho()) as CreatedAtRouteResult;

            Assert.IsNotNull(resultadoCriacao);

            var usuarioCriado = resultadoCriacao.Value as Usuario;

            Assert.IsNotNull(usuarioCriado);

            var resultadoBusca = controller.BuscarUsuarioPorLogin(usuarioCriado.Login);

            Assert.IsNotNull(resultadoBusca as OkObjectResult);
        }

        [TestMethod]
        public void Usuarios_Removidos_Nao_Devem_Ser_Retornados_No_Obter_Por_Login()
        {
            var usuario = CriarFulaninho();

            var usuarioController = new UsuariosController();

            var resultadoCriacao = usuarioController.CriarUsuario(usuario) as CreatedAtRouteResult;

            Assert.IsNotNull(resultadoCriacao);

            var usuarioCriado = resultadoCriacao.Value as Usuario;

            var usuarioRemovido = usuarioController.RemoverUsuario(usuarioCriado.Login);

            var usuarioRetornadoNoGet = usuarioController.BuscarUsuarioPorLogin(usuarioCriado.Login) as NotFoundObjectResult;

            Assert.IsNotNull(usuarioRetornadoNoGet);
        }

        [TestMethod]
        public void Todos_Usuarios_Cadastrados_Devem_Ser_Retornados_No_Obter_Todos()
        {
            var teddy = CriarFulaninho();
            var joazinho = CriarJoaozinho();

            var usuarioController = new UsuariosController();

            var resultadoCriacaoTeddy = usuarioController.CriarUsuario(teddy);

            var resultadoCriacaoJoazinho = usuarioController.CriarUsuario(joazinho);

            var resultadoBuscaUsuarios = usuarioController.BuscarUsuarios() as OkObjectResult;

            var usuarios = resultadoBuscaUsuarios.Value as List<Usuario>;

            Assert.AreEqual(2, usuarios.Count);
        }

        [TestMethod]
        public void Atualizar_Usuario_Deve_Retornar_Erro_Quando_O_Usuario_For_Nulo()
        {
            var controller = new UsuariosController();

            var badRequest = controller.AlterarUsuario("teste", null) as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O parametro usuarioAtualizado não pode ser nulo", badRequest.Value);
        }

        [TestMethod]
        public void Atualizar_Usuario_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = new UsuariosController();

            var badRequest = controller.AlterarUsuario("teste", new UsuarioComSenha()) as NotFoundObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O usuario teste não foi encontrado", badRequest.Value);
        }

        [TestMethod]
        public void Remover_Usuario_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = new UsuariosController();

            var badRequest = controller.RemoverUsuario("teste") as NotFoundObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O usuario teste não foi encontrado", badRequest.Value);
        }

        [TestMethod]
        public void Pesquisar_Um_Usuario_Por_Um_Id_Inexistente_Deve_Retornar_Not_Found()
        {
            var controller = new UsuariosController();

            var resultadoBusca = controller.BuscarUsuarioPorLogin("teste");

            Assert.IsNotNull(resultadoBusca as NotFoundObjectResult);
        }

        [TestMethod]
        public void Login_E_Logout_Devem_Retornar_Ok_Quando_Os_Dados_Estiverem_Corretos()
        {
            var usuario = CriarFulaninho();

            var controller = new UsuariosController();

            controller.CriarUsuario(usuario);

            var loginComSucesso = controller.LoginELogout(new DadosLogin() { Login = "fulaninho", Senha = "fulaninho" }) as OkResult;

            Assert.IsNotNull(loginComSucesso);
        }

        [TestMethod]
        public void Login_E_Logout_Devem_Retornar_Erro_Quando_O_Usuario_Estiver_Incorreto()
        {
            var usuario = CriarFulaninho();

            var controller = new UsuariosController();

            var usuarioCriado = controller.CriarUsuario(usuario);

            Assert.IsNotNull(usuarioCriado);

            var badRequest = controller.LoginELogout(new DadosLogin() { Login = "fulaninho", Senha = "fulaninho123" }) as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Usuario ou senha inválidos", badRequest.Value);
        }

        [TestMethod]
        public void Login_E_Logout_Devem_Retornar_Erro_Quando_A_Senha_Estiver_Incorreta()
        {
            var usuario = CriarFulaninho();

            var controller = new UsuariosController();

            var usuarioCriado = controller.CriarUsuario(usuario);

            Assert.IsNotNull(usuarioCriado);

            var badRequest = controller.LoginELogout(new DadosLogin() { Login = "fulaninhoo", Senha = "fulaninho" }) as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Usuario ou senha inválidos", badRequest.Value);
        }

        [TestMethod]
        public void Login_E_Logout_Devem_Retornar_Erro_Quando_Null_For_Informado()
        {
            var controller = new UsuariosController();

            var badRequest = controller.LoginELogout(null) as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("O parametro dadosLogin não pode ser null", badRequest.Value);
        }

        private UsuarioComSenha CriarFulaninho()
        {
            return new UsuarioComSenha()
            {
                Login = "fulaninho",
                Senha = "fulaninho",
                PrimeiroNome = "Fulaninho",
                UltimoNome = "Silva",
                Email = "fulaninho@cwi.com.br",
                Telefone = "518874654255",
                StatusUsuario = StatusUsuario.Ativo,
            };
        }

        private UsuarioComSenha CriarJoaozinho()
        {
            return new UsuarioComSenha()
            {
                Login = "joaozinho",
                Senha = "joaozinho",
                PrimeiroNome = "Joaozinho",
                UltimoNome = "Silva",
                Email = "joaozinho@cwi.com.br",
                Telefone = "518874563255",
                StatusUsuario = StatusUsuario.Inativo,
            };
        }
    }
}