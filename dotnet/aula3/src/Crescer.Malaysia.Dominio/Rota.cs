using Geolocation;

namespace Crescer.Malaysia.Dominio
{
    public class Rota
    {
        public Rota(Ponto pontoA, Ponto pontoB)
        {
            PontoA = pontoA;
            PontoB = pontoB;
        }
        public Ponto PontoA { get; private set; }
        public Ponto PontoB { get; private set; }

        public double CalcularDistancia()
        {
            int casasDecimais = 1;
            return GeoCalculator.GetDistance(PontoA.Local.Latitude, PontoA.Local.Longitude, PontoB.Local.Latitude, PontoB.Local.Longitude, casasDecimais, DistanceUnit.Miles);
        }
    }
}