using Crescer.Malaysia.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crescer.Malaysia.Tests
{
    [TestClass]
    public class RotaTest
    {
        [TestMethod]
        public void TestarCalcularDistancia()
        {
            //Arrange
            Ponto pontoA = new Ponto(-29.36071, -50.83593);
            Ponto pontoB = new Ponto(-29.36027, -50.83937);
            Rota rota = new Rota(pontoA, pontoB);
            double esperado = 0.2;
            //Act
            double retornado = rota.CalcularDistancia();
            //Assert
            Assert.AreEqual(esperado, retornado);
        }
        [TestMethod]
        public void TestarCalcularDistanciaIntercontinental()
        {
            //Arrange
            Ponto pontoA = new Ponto(-28.86843, -51.13244);
            Ponto pontoB = new Ponto(41.89, 12.492);
            Rota rota = new Rota(pontoA, pontoB);
            double esperado = 6348.5;
            //Act
            double retornado = rota.CalcularDistancia();
            //Assert
            Assert.AreEqual(esperado, retornado);
        }
    }
}
