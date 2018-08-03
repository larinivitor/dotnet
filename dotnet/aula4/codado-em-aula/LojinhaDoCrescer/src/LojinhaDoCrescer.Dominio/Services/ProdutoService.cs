using System.Collections.Generic;
using LojinhaDoCrescer.Dominio.Entidades;

namespace LojinhaDoCrescer.Dominio.Services
{
    public class ProdutoService
    {
        public List<string> VerificarInconsistenciasEmUmNovoProduto(Produto produto)
        {
            var inconsistencias = new List<string>();

            if (produto.Valor <= 0)
                inconsistencias.Add($"O campo {nameof(produto.Valor)} deve ser maior que zero");

            if (string.IsNullOrEmpty(produto.Descricao))
            {
                inconsistencias.Add($"O campo {nameof(produto.Descricao)} não pode ser nulo");
                return inconsistencias;
            }

            if (!produto.Descricao.Contains("REF:"))
                inconsistencias.Add($"A descrição deve conter a referencia do produto");

            return inconsistencias;
        }
    }
}