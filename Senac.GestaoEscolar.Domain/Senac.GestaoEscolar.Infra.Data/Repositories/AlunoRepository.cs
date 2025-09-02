using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AlunoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IEnumerable<Aluno>, int)> ObterTodosAlunos(int pagina, int limite)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var offset = (pagina - 1) * limite;

                var sqlAlunos = @"
                    SELECT Id, Nome, Sobrenome 
                    FROM Aluno 
                    ORDER BY Nome 
                    OFFSET @Offset ROWS 
                    FETCH NEXT @Limite ROWS ONLY";

                var sqlTotal = "SELECT COUNT(*) FROM Aluno";

                var alunos = await connection.QueryAsync<Aluno>(sqlAlunos, new { Offset = offset, Limite = limite });
                var totalDeRegistros = await connection.ExecuteScalarAsync<int>(sqlTotal);

                return (alunos, totalDeRegistros);
            }
        }

        public async Task<Aluno> ObterAluno(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT * FROM Aluno WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Aluno>(sql, new { Id = id });
            }
        }

        public async Task<long> CadastrarAluno(Aluno aluno)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
                    INSERT INTO Aluno (nome, sobrenome, dataDeNascimento, email, telefone, dataMatricula, ativo)
                    OUTPUT INSERTED.Id
                    VALUES (@Nome, @Sobrenome, @DataDeNascimento, @Email, @Telefone, @DataMatricula, @Ativo);";

                return await connection.QuerySingleAsync<long>(sql, aluno);
            }
        }

        public async Task AtualizarAluno(Aluno aluno)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
                    UPDATE Aluno
                    SET 
                        email = @Email,
                        telefone = @Telefone,
                        ativo = @Ativo
                    WHERE Id = @Id";

                await connection.ExecuteAsync(sql, aluno);
            }
        }

        public async Task DeletarAluno(long id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM Aluno WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}