using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojinhaDoCrescer.Api.Models;
using LojinhaDoCrescer.Dominio.Contratos;
using LojinhaDoCrescer.Dominio.Entidades;
using LojinhaDoCrescer.Dominio.Services;
using LojinhaDoCrescer.Infra;
using Microsoft.AspNetCore.Mvc;

namespace LojinhaDoCrescer.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private IProdutoRepository produtoRepository;

        private ProdutoService produtoService;

        public ProdutoController(IProdutoRepository produtoRepository, ProdutoService produtoService)
        {
            this.produtoService = produtoService;
            this.produtoRepository = produtoRepository;
        }

        // GET api/produto/5
        [HttpGet("{id}", Name = "GetProduto")]
        public ActionResult Get(int id)
        {
            var produto = produtoRepository.BuscarPorId(id);

            if (produto == null) return NotFound("Não encontrado um produto com o id informado");

            return Ok(produto);
        }

        // POST api/produto
        [HttpPost]
        public ActionResult Post([FromBody]ProdutoRequestDTO produtoDto)
        {
            var produto = new Produto(produtoDto.Descricao, produtoDto.Valor);

            var inconsistencias = produtoService.VerificarInconsistenciasEmUmNovoProduto(produto);

            if (inconsistencias.Any()) return BadRequest(inconsistencias);

            produtoRepository.Salvar(produto);

            return CreatedAtRoute("GetProduto", new { id = produto.Id }, produto);
        }
    }
}