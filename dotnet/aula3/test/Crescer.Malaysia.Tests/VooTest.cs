using Crescer.Malaysia.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crescer.Malaysia.Tests
{
    [TestClass]
    public class VooTest
    {
        [TestMethod]
        public void TestarCalcularValorTotalVooAviaoJato()
        {
            //Arrange
            Aviao aviaoJato = new TurboJato("Boeing 727", 2, 50);
            Ponto pontoA = new Ponto(-28.86843, -51.13244);
            Ponto pontoB = new Ponto(41.89, 12.492);
            Rota rota = new Rota(pontoA, pontoB);
            Voo voo =  new Voo(rota, aviaoJato);
            double esperado = 952275;
            //Act
            double retornado = voo.CalcularValorTotalVoo();
            //Assert
            Assert.AreEqual(esperado, retornado);
        }
        [TestMethod]
        public void TestarCalcularValorTotalVooAviaoTurboElice()
        {
             //Arrange
            Aviao aviaoJato = new Turboelice("ATR-72", 2, 50);
            Ponto pontoA = new Ponto(-28.86843, -51.13244);
            Ponto pontoB = new Ponto(41.89, 12.492);
            Rota rota = new Rota(pontoA, pontoB);
            Voo voo =  new Voo(rota, aviaoJato);
            double esperado = 761820;
            //Act
            double retornado = voo.CalcularValorTotalVoo();
            //Assert
            Assert.AreEqual(esperado, retornado);
        }

    }
}