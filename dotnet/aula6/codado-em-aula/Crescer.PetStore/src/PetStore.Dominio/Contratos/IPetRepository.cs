using System.Collections.Generic;
using PetStore.Dominio.Entidades;

namespace PetStore.Dominio.Contratos
{
    public interface IPetRepository
    {
        Pet Cadastrar(Pet pet);

        Pet Alterar(int id, Pet pet);

        Pet Remover(int id);

        Pet ObterPorId(int id);

        Categoria ObterCategoriaPorId(int id);

        List<Pet> ObterTodos();
    }
}