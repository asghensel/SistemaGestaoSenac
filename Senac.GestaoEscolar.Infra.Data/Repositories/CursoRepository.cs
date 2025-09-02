using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public CursoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IEnumerable<Curso>, int)> ObterTodosCursos(int pagina, int limite)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var offset = (pagina - 1) * limite;
                // NOME DA TABELA CORRIGIDO
                var sqlCursos = @"
                    SELECT Id, Nome, CargaHoraria 
                    FROM Curso 
                    ORDER BY Nome 
                    OFFSET @Offset ROWS 
                    FETCH NEXT @Limite ROWS ONLY";

                // NOME DA TABELA CORRIGIDO
                var sqlTotal = "SELECT COUNT(*) FROM Curso";

                var cursos = await connection.QueryAsync<Curso>(sqlCursos, new { Offset = offset, Limite = limite });
                var totalDeRegistros = await connection.ExecuteScalarAsync<int>(sqlTotal);

                return (cursos, totalDeRegistros);
            }
        }

        public async Task<Curso> ObterCurso(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = "SELECT * FROM Curso WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Curso>(sql, new { Id = id });
            }
        }

        public async Task<long> CadastrarCurso(Curso curso)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = @"
                    INSERT INTO Curso (Nome, Descricao, DataCriacao, CargaHoraria, Categoria, Valor, Ativo, ProfessorId) 
                    OUTPUT INSERTED.Id 
                    VALUES (@Nome, @Descricao, @DataCriacao, @CargaHoraria, @Categoria, @Valor, @Ativo, @ProfessorId);";
                return await connection.QuerySingleAsync<long>(sql, curso);
            }
        }

        public async Task AtualizarCurso(Curso curso)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = @"
                    UPDATE Curso SET 
                        Descricao = @Descricao, CargaHoraria = @CargaHoraria, Valor = @Valor, 
                        ProfessorId = @ProfessorId, Ativo = @Ativo, Categoria = @Categoria 
                    WHERE Id = @Id";
                await connection.ExecuteAsync(sql, curso);
            }
        }

        public async Task DeletarCurso(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = "DELETE FROM Curso WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}