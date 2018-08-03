using LojinhaDoCrescer.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LojinhaDoCrescer.Dominio.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void O_Valor_Total_Do_Carrinho_Deve_Ser_Calculado_Corretamenta()
        {
            var carrinho = new Carrinho();

            var produto = new Produto("Notebook Dell", 5000);

            carrinho.AdicionarProduto(produto, 10);

            Assert.AreEqual(50000, carrinho.ValorTotal);            
        }
    }
}
