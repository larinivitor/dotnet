using System;
using System.Collections.Generic;

namespace PetStore.Dominio.Entidades
{
    public class Pet
    {
        private Pet() { }

        public Pet(string nome, StatusPet status, Categoria categoria, List<Tag> tags)
        {
            this.Tags = tags;
            this.Nome = nome;
            this.Status = status;
            this.Categoria = categoria;
        }

        public int Id { get; private set; }

        public string Nome { get; private set; }

        public Categoria Categoria { get; private set; }

        public List<Tag> Tags { get; private set; }

        public StatusPet Status { get; private set; }

        public void Atualizar(Pet pet)
        {
            Nome = pet.Nome;
            Categoria = pet.Categoria;
            Tags = pet.Tags;
            Status = pet.Status;
        }
    }
}