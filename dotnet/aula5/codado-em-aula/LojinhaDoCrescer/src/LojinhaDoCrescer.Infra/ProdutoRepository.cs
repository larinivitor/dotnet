using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using LojinhaDoCrescer.Dominio.Contratos;
using LojinhaDoCrescer.Dominio.Entidades;

namespace LojinhaDoCrescer.Infra
{
    public class ProdutoRepository : IProdutoRepository
    {
        private Database database;
        public ProdutoRepository(Database database)
        {
            this.database = database;
        }

        public void Atualizar(int id, Produto produto)
        {
            database.Connection.Execute(@"
                UPDATE [dbo].[Produto] SET
                    [Descricao] = @Descricao,
                    [Valor] = @Valor                 
                WHERE
                    [Id] = @Id", new { id, produto.Descricao, produto.Valor }, database.Transaction);
        }

        public Produto BuscarPorId(int id)
        {
            return database.Connection.Query<Produto>(@"
                SELECT   
                     [Id]
                    ,[Descricao]
                    ,[Valor]
                FROM [dbo].[Produto]
                WHERE
                    [Id] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void Salvar(Produto produto)
        {
            int id = database.Connection.Query<int>(@"
                INSERT INTO [dbo].[Produto]
                    ([Descricao]
                    ,[Valor])
                VALUES
                    (
                      @Descricao
                     ,@Valor);
                SELECT CAST(SCOPE_IDENTITY() as int)", new { produto.Descricao, produto.Valor }, database.Transaction).Single();

            produto.Id = id;
        }
    }
}