using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.WebApi.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Authorize, Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private UsuarioService usuarioService;
        private AvaliacaoService avaliacaoService;
        private IMusicaRepository musicaRepository;
        private SpotifyContext contexto;
        private IOptions<SecuritySettings> settings;

        public UsuarioController(IUsuarioRepository usuarioRepository,
                UsuarioService usuarioService,
                IMusicaRepository musicaRepository,
                AvaliacaoService avaliacaoService,
                SpotifyContext contexto,
                IOptions<SecuritySettings> settings)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.avaliacaoService = avaliacaoService;
            this.musicaRepository = musicaRepository;
            this.contexto = contexto;
            this.settings = settings;
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
        [AllowAnonymous, HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var mensagens = usuarioService.Validar(usuario);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            usuarioRepository.SalvarUsuario(usuario);
            contexto.SaveChanges();
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
            contexto.SaveChanges();
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
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            usuarioRepository.DeletarUsuario(id);
            contexto.SaveChanges();
            return Ok();
        }

        [AllowAnonymous, HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dadosLogin)
        {
            var usuario = usuarioRepository.ObterUsuarioPorLoginESenha(dadosLogin.Login, dadosLogin.Senha);

            if (usuario == null) return BadRequest("Usuario ou senha inválidos");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SigningKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, "Admin"),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        private Usuario MapearDtoParaDominio(UsuarioDto usuarioRequest)
        {
            return new Usuario(usuarioRequest.Nome, usuarioRequest.Login, usuarioRequest.Senha);
        }
    }
}