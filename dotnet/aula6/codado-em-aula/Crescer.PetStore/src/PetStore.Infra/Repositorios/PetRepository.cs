using System.Collections.Generic;
using PetStore.Dominio.Contratos;
using PetStore.Dominio.Entidades;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PetStore.Infra.Repositorios
{
    public class PetRepository : IPetRepository
    {
        private PetStoreContext contexto;

        public PetRepository(PetStoreContext contexto)
        {
            this.contexto = contexto;
        }

        public Pet Alterar(int id, Pet pet)
        {
            var petCadastrado = contexto.Pets.FirstOrDefault(p => p.Id == id);

            if (petCadastrado != null)
                petCadastrado.Atualizar(pet);

            return petCadastrado;
        }

        public Pet Cadastrar(Pet pet)
        {
            contexto.Pets.Add(pet);
            return pet;
        }

        public Pet ObterPorId(int id)
        {
            return contexto.Pets
                        .Include(p => p.Categoria)
                        .Include(p => p.Tags)
                        .AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Categoria ObterCategoriaPorId(int id)
        {
            return contexto.Categorias.FirstOrDefault(p => p.Id == id);
        }

        public List<Pet> ObterTodos()
        {
            return contexto.Pets.AsNoTracking().ToList();
        }

        public Pet Remover(int id)
        {
            var petCadastrado = contexto.Pets.FirstOrDefault(p => p.Id == id);

            if (petCadastrado != null)
                contexto.Pets.Remove(petCadastrado);

            return petCadastrado;
        }
    }
}