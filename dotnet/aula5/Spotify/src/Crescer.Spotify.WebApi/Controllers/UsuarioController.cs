using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private UsuarioService usuarioService;
        private AvaliacaoService avaliacaoService;
        private IMusicaRepository musicaRepository;
        private Database database;

        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioService usuarioService, Database database, IMusicaRepository musicaRepository, AvaliacaoService avaliacaoService)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.avaliacaoService = avaliacaoService;
            this.database = database;
            this.musicaRepository = musicaRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(usuarioRepository.ListarUsuario());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult Get(int id)
        {
            var usuario = usuarioRepository.Obter(id);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.SalvarUsuario(usuario);
            database.Commit();
            return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPost("avaliacao")]
        public IActionResult Avaliar([FromBody]AvaliacaoDto avaliacaoRequest)
        {
            var usuario = usuarioRepository.Obter(avaliacaoRequest.IdUsuario);
            if (usuario == null) return NotFound();
            var musica = musicaRepository.Obter(avaliacaoRequest.IdMusica);
            if (musica == null) return NotFound();

            var mensagens = avaliacaoService.Validar(avaliacaoRequest.Nota);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.Avaliar(avaliacaoRequest.IdUsuario, avaliacaoRequest.IdMusica, avaliacaoRequest.Nota);
            database.Commit();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.AtualizarUsuario(id, usuario);
            database.Commit();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            usuarioRepository.DeletarUsuario(id);
            database.Commit();
            return Ok();
        }

        private Usuario MapearDtoParaDominio(UsuarioDto usuarioRequest)
        {
            return new Usuario(usuarioRequest.Nome);
        }
    }
}