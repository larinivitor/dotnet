namespace Crescer.Malaysia.Dominio
{
    public class Turboelice : Aviao
    {
        private const double percentualCalculoPorMotor = 1.2;

        public Turboelice(string nome, int quantidadeMotor, double valor) : base(nome, quantidadeMotor, valor)
        {
        }

        public override double CalcularValorBase()
        {
            return Valor * (QuantidadeMotor * percentualCalculoPorMotor);
        }
    }
}