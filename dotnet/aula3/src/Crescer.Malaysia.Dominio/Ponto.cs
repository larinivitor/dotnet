using System;
using Geolocation;

namespace Crescer.Malaysia.Dominio
{
    public class Ponto
    {
        public Ponto(double latitude, double longitude)
        {
            Local = new Coordinate()
            {
                Latitude = latitude,
                Longitude = longitude
            };      
        }
        public Coordinate Local { get; private set; }
    }
}
