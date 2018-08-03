using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private Database database;        
        public AlbumRepository(Database database)
        {
            this.database = database;           
        }
        public void AtualizarAlbum(int id, Album album)
        {
            database.Connection.Execute(@"
                UPDATE [dbo].[Album]
                    SET [Nome] = @Nome
                WHERE [Id] = @Id", new { Nome = album.Nome, Id = id }, database.Transaction);
        }

        public void DeletarAlbum(int id)
        {
            database.Connection.Execute(@"
                DELETE [A] FROM [dbo].[Avaliacao] [A]
				INNER JOIN [dbo].[Musica] [M] ON [A].[IdMusica] = [M].[Id] AND [M].[IdAlbum] = @Id;			
                DELETE [dbo].[Musica] WHERE [IdAlbum] = @Id;
                DELETE FROM [dbo].[Album] WHERE [Id] = @Id;", new { id }, database.Transaction);
        }

        public List<Album> ListarAlbum()
        {
            return database.Connection.Query<Album>(@"
                        SELECT 
                             [A].[Id]		
	                        ,[A].[Nome]                                                     
                        FROM 
                            [dbo].[Album] [A]", null, database.Transaction).ToList();
        }

        public Album Obter(int id)
        {
            Album album = database.Connection.Query<Album>(@"
                        SELECT 
                             [A].[Id]		
	                        ,[A].[Nome]                                                     
                        FROM 
                            [dbo].[Album] [A]                       
                        WHERE [A].[Id] = @Id", new { id }, database.Transaction).FirstOrDefault();

            List<Musica> musicas = database.Connection.Query<Musica>(@"
                    SELECT [Id]
                        ,[Nome]
                        ,[Duracao]                     
                    FROM 
                        [dbo].[Musica]
                    WHERE 
                        [IdAlbum] = @IdAlbum", new { IdAlbum = id }, database.Transaction).ToList();

            album?.Atualizar(album, musicas);
            return album;
        }

        public double ObterAvaliacao(int id)
        {
            return database.Connection.Query<double>(@"
                    SELECT 
                        ISNULL((SUM(A.[Nota]) / COUNT(1)), 0) AS Nota
                    FROM 
                        [dbo].[Musica] [M]
                    INNER JOIN 
                        [dbo].[Avaliacao] [A] ON [A].[IdMusica] = [M].[Id]
                    WHERE 
                    [M].[IdAlbum] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void SalvarAlbum(Album album)
        {
            var id = database.Connection.Query<int>(@"
                    INSERT INTO [dbo].[Album]
                        ([Nome])
                    VALUES
                        (@Nome);                
                    SELECT CAST(SCOPE_IDENTITY() as int)", new { album.Nome }, database.Transaction).Single();

            album.Id = id;
        }
    }
}