namespace Crescer.Malaysia.Dominio
{
    public class TurboJato : Aviao
    {        
        private const double percentualCalculoPorJato = 1.5;

        public TurboJato(string nome, int quantidadeMotor, double valor) : base(nome, quantidadeMotor, valor)
        {
        }

        public override double CalcularValorBase()
        {
            return Valor * (QuantidadeMotor * percentualCalculoPorJato);
        }
    }
}