using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Professores;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProfessorRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IEnumerable<Professor>, int)> ObterTodosProfessores(int pagina, int limite)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var offset = (pagina - 1) * limite;
                // NOME DA TABELA CORRIGIDO
                var sqlProfessores = @"
                    SELECT Id, Nome, Sobrenome 
                    FROM Professor 
                    ORDER BY Nome 
                    OFFSET @Offset ROWS 
                    FETCH NEXT @Limite ROWS ONLY";

                // NOME DA TABELA CORRIGIDO
                var sqlTotal = "SELECT COUNT(*) FROM Professor";

                var professores = await connection.QueryAsync<Professor>(sqlProfessores, new { Offset = offset, Limite = limite });
                var totalDeRegistros = await connection.ExecuteScalarAsync<int>(sqlTotal);

                return (professores, totalDeRegistros);
            }
        }

        public async Task<Professor> ObterProfessorPorId(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = "SELECT * FROM Professor WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Professor>(sql, new { Id = id });
            }
        }

        public async Task<long> CadastrarProfessor(Professor professor)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = @"
                    INSERT INTO Professor (Nome, Sobrenome, DataDeNascimento, Email, Telefone, Formacao, DataContratacao, Ativo) 
                    OUTPUT INSERTED.Id
                    VALUES (@Nome, @Sobrenome, @DataDeNascimento, @Email, @Telefone, @Formacao, @DataContratacao, @Ativo);";
                return await connection.ExecuteScalarAsync<long>(sql, professor);
            }
        }

        public async Task AtualizarProfessor(Professor professor)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = @"
                    UPDATE Professor 
                    SET Email = @Email, Telefone = @Telefone, Ativo = @Ativo, Formacao = @Formacao 
                    WHERE Id = @Id";
                await connection.ExecuteAsync(sql, professor);
            }
        }

        public async Task DeletarProfessor(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                // NOME DA TABELA CORRIGIDO
                var sql = "DELETE FROM Professor WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}