namespace Crescer.Malaysia.Dominio
{
    public abstract class Aviao
    {
        public Aviao(string nome, int quantidadeMotor, double valor)
        {
            Nome = nome;
            QuantidadeMotor = quantidadeMotor;
            Valor = valor;
        }
        public string Nome { get; private set; }
        public int QuantidadeMotor { get; private set; }
        public double Valor { get; private set; }

        public abstract double CalcularValorBase();

    }
}