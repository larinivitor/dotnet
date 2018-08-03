namespace Crescer.Malaysia.Dominio
{
    public class Voo
    {
        public Rota Rota { get; private set; }
        public Aviao Aviao { get; private set; }
        
        public Voo(Rota rota, Aviao aviao)
        {
            Rota = rota;  
            Aviao = aviao;          
        }

        public double CalcularValorTotalVoo()
        {
            return Aviao.CalcularValorBase() * Rota.CalcularDistancia();
        }
    }
}