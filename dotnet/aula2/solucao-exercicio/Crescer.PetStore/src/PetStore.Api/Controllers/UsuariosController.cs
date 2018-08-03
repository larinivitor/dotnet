using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetStore.Api.Database;
using PetStore.Api.Models;

namespace UsuarioStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private static readonly string DatabasePath = $"{Path.GetTempPath()}usuario-database.json";

        public UsuariosController()
        {
            if (!System.IO.File.Exists(DatabasePath))
                System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(new UsuarioDatabase()
                {
                    Id = 1,
                    Usuarios = new List<UsuarioComSenha>()
                }));
        }

        [HttpGet]
        public IActionResult BuscarUsuarios()
        {
            var database = CarregarDatabase();

            return Ok(database.Usuarios.Select(usuario => new Usuario(usuario)).ToList());
        }

        [HttpGet("{login}", Name = "GetUsuario")]
        public IActionResult BuscarUsuarioPorLogin(string login)
        {
            var database = CarregarDatabase();

            var usuario = database.Usuarios.FirstOrDefault(x => x.Login == login);

            if (usuario == null) return NotFound($"O usuario {login} não foi encontrado");

            return Ok(new Usuario(usuario));
        }

        [HttpPost]
        public IActionResult CriarUsuario([FromBody]UsuarioComSenha usuario)
        {
            var database = CarregarDatabase();

            if (usuario == null) return BadRequest($"O parametro {nameof(usuario)} não pode ser nulo");

            if (database.Usuarios.Any(x => x.Login == usuario.Login))
                return BadRequest($"O login {usuario.Login} já está sendo utilizado");

            usuario.Id = database.Id++;
            database.Usuarios.Add(usuario);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return CreatedAtRoute("GetUsuario", new { login = usuario.Login }, new Usuario(usuario));
        }

        [HttpPut("{login}")]
        public IActionResult AlterarUsuario(string login, [FromBody]UsuarioComSenha usuarioAtualizado)
        {
            var database = CarregarDatabase();

            if (usuarioAtualizado == null) return BadRequest($"O parametro {nameof(usuarioAtualizado)} não pode ser nulo");

            usuarioAtualizado.Login = login;

            var usuario = database.Usuarios.FirstOrDefault(x => x.Login == login);

            if (usuario == null) return NotFound($"O usuario {login} não foi encontrado");

            usuarioAtualizado.Id = usuario.Id;

            database.Usuarios.Remove(usuario);
            database.Usuarios.Add(usuarioAtualizado);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return Ok(new Usuario(usuarioAtualizado));
        }

        [HttpDelete("{login}")]
        public IActionResult RemoverUsuario(string login)
        {
            var database = CarregarDatabase();

            var usuario = database.Usuarios.FirstOrDefault(x => x.Login == login);

            if (usuario == null) return NotFound($"O usuario {login} não foi encontrado");

            database.Usuarios.Remove(usuario);

            System.IO.File.WriteAllText(DatabasePath, JsonConvert.SerializeObject(database));

            return Ok(new Usuario(usuario));
        }

        [HttpPost("login")]
        [HttpPost("logout")]
        public IActionResult LoginELogout([FromBody]DadosLogin dadosLogin)
        {
            var database = CarregarDatabase();

            if (dadosLogin == null)
                return BadRequest($"O parametro {nameof(dadosLogin)} não pode ser null");

            var usuario = database.Usuarios.FirstOrDefault(x => x.Login == dadosLogin.Login);

            if (usuario == null || usuario.Senha != dadosLogin.Senha)
                return BadRequest($"Usuario ou senha inválidos");

            return Ok();
        }

        private UsuarioDatabase CarregarDatabase()
        {
            return JsonConvert.DeserializeObject<UsuarioDatabase>(System.IO.File.ReadAllText(DatabasePath));
        }
    }
}