using System.Collections.Generic;
using LojinhaDoCrescer.Dominio.Entidades;

namespace LojinhaDoCrescer.Dominio.Contratos
{
    public interface IProdutoRepository
    {
        void Salvar(Produto produto);

        Produto BuscarPorId(int id);
    }
}