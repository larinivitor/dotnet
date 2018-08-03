using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.WebApi.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Crescer.Spotify.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private IAlbumRepository albumRepository;
        
        private IMusicaRepository musicaRepository;
        
        private AlbumService albumService;
        
        private SpotifyContext contexto;

        public AlbumController(IAlbumRepository albumRepository, IMusicaRepository musicaRepository, AlbumService albumService, SpotifyContext contexto)
        {
            this.albumRepository = albumRepository;
            this.musicaRepository = musicaRepository;
            this.albumService = albumService;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(albumRepository.ListarAlbums());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetAlbum")]
        public IActionResult Get(int id)
        {
            var album = albumRepository.Obter(id);

            if (album == null) return NotFound();

            return Ok(album);
        }

        [HttpGet("{id}/avaliacao")]
        public IActionResult GetAvaliacao(int id)
        {
            var album = albumRepository.Obter(id);
            if (album == null) return NotFound();

            double nota = albumRepository.ObterAvaliacao(id);

            return Ok(nota);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumRepository.SalvarAlbum(album);
            contexto.SaveChanges();
            return CreatedAtRoute("GetAlbum", new { id = album.Id }, album);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AlbumDto albumRequest)
        {
            var album = MapearDtoParaDominio(albumRequest);
            var mensagens = albumService.Validar(album);
            if (mensagens.Count > 0)
                return BadRequest(mensagens);

            albumRepository.AtualizarAlbum(id, album);
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            albumRepository.DeletarAlbum(id);
            contexto.SaveChanges();
            return Ok();
        }

        private Album MapearDtoParaDominio(AlbumDto album)
        {
            return new Album(album.Nome);
        }
    }
}