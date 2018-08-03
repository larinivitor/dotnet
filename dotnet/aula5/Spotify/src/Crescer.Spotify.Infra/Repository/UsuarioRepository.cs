using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Dapper;

namespace Crescer.Spotify.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Database database;
        public UsuarioRepository(Database database)
        {
            this.database = database;
        }
        public void AtualizarUsuario(int id, Usuario usuario)
        {
            database.Connection.Execute(@"
                UPDATE [dbo].[Usuario]
                    SET [Nome] = @Nome
                WHERE [Id] = @Id", new { Nome = usuario.Nome, Id = id }, database.Transaction);
        }

        public void Avaliar(int idUsuario, int idMusica, int nota)
        {
            database.Connection.Execute(@"
                MERGE 
                    [dbo].[Avaliacao] AS target
	            USING 
                    (SELECT @IdUsuario, @IdMusica) AS source (IdUsuario, IdMusica)
	            ON 
                    (target.IdUsuario = source.IdUsuario AND target.IdMusica = source.IdMusica)
	            WHEN MATCHED THEN 
		        UPDATE SET
			        IdUsuario = @IdUsuario,
			        IdMusica  = @IdMusica,
			        Nota      = @Nota
	            WHEN NOT MATCHED THEN
		            INSERT (
			            IdUsuario,
			            IdMusica,
			            Nota
			        )			
		            VALUES (
                        @IdUsuario,
		                @IdMusica,
                        @Nota
                	);", new { idUsuario, idMusica, nota }, database.Transaction);
        }

        public void DeletarUsuario(int id)
        {
            database.Connection.Execute(@"
                DELETE [A] FROM [dbo].[Avaliacao] [A]
                INNER JOIN [dbo].[Usuario] [U] ON [U].[Id] = [A].[IdUsuario] AND [U].[Id] = @Id;
                DELETE [dbo].[Usuario] WHERE [Id] = @Id;", new { id }, database.Transaction);
        }

        public List<Usuario> ListarUsuario()
        {
            return database.Connection.Query<Usuario>(@"
                        SELECT 
                             [Id]		
	                        ,[Nome]
                        FROM 
                            [dbo].[Usuario]", null, database.Transaction).ToList();
        }

        public Usuario Obter(int id)
        {
            return database.Connection.Query<Usuario>(@"
                        SELECT 
                             [Id]		
	                        ,[Nome]                                                     
                        FROM 
                            [dbo].[Usuario]
                        WHERE [Id] = @Id", new { id }, database.Transaction).FirstOrDefault();
        }

        public void SalvarUsuario(Usuario usuario)
        {
            var id = database.Connection.Query<int>(@"
                    INSERT INTO [dbo].[Usuario]
                        ([Nome])
                    VALUES
                        (@Nome);                
                    SELECT CAST(SCOPE_IDENTITY() as int)", new { usuario.Nome }, database.Transaction).Single();

            usuario.Id = id;
        }
    }
}