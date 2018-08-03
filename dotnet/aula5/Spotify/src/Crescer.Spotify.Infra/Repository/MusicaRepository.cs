using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {
        private Database database;
        public MusicaRepository(Database database)
        {
            this.database = database;
        }
        public void AtualizarMusica(int id, Musica musica)
        {
            database.Connection.Execute(@"
                UPDATE [dbo].[Musica] SET 
                     [Nome] = @Nome
                    ,[Duracao] = @Duracao                   
                WHERE 
                    [Id] = @Id", new { musica.Nome, musica.Duracao, id }, database.Transaction);
        }

        public void DeletarMusica(int id)
        {
            database.Connection.Execute(@"
                DELETE [A] FROM [dbo].[Avaliacao] [A]
				INNER JOIN [dbo].[Musica] [M] ON [A].[IdMusica] = [M].[Id] AND [M].[Id] = @Id;			
                DELETE [dbo].[Musica] WHERE [Id] = @Id;", new { id }, database.Transaction);
        }

        public List<Musica> ListarMusicas(int idAlbum)
        {
            return database.Connection.Query<Musica>(@"
                    SELECT 
                         [Id]
                        ,[Nome]
                        ,[Duracao]                     
                    FROM 
                        [dbo].[Musica]
                    WHERE [IdAlbum] = @IdAlbum", new { idAlbum }, database.Transaction).ToList();
        }

        public Musica Obter(int id)
        {
            return database.Connection.Query<Musica>(@"
                    SELECT [Id]
                        ,[Nome]
                        ,[Duracao]                     
                    FROM 
                        [dbo].[Musica]
                    WHERE [Id] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public double ObterAvaliavao(int id)
        {
            return database.Connection.Query<double>(@"
                    SELECT   
                        (SUM([Nota]) / COUNT(1)) AS Nota
                    FROM 
                        [Spotify].[dbo].[Avaliacao] 
                    WHERE [IdMusica] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void SalvarMusica(int idAlbum, Musica musica)
        {
            var id = database.Connection.Query<int>(@"
                    INSERT INTO [dbo].[Musica]
                        ([Nome], [Duracao], [IdAlbum]) 
                    VALUES
                        (@Nome, @Duracao, @IdAlbum);
                        
                    SELECT CAST(SCOPE_IDENTITY() as int)", new { musica.Nome, musica.Duracao, IdAlbum = idAlbum }, database.Transaction).Single();

            musica.Id = id;
        }
    }
}