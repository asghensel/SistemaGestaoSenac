using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public AlunoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AtualizarAluno( Aluno aluno)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync(
                @"
                 UPDATE Aluno
                    SET 
                        email = @Email,
                        telefone = @Telefone,
                        ativo = @Ativo
                    WHERE Id = @Id
                    ", aluno);
        }

        public async Task<long> CadastrarAluno(Aluno aluno)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<long>(
                @"
                 INSERT INTO Aluno
                    (nome,
                    sobrenome,
                    dataDeNascimento,
                    email,
                    telefone,
                    dataMatricula,
                    ativo)
                    OUTPUT INSERTED.Id
                    VALUES 
                        (@Nome,
                        @Sobrenome,
                        @DataDeNascimento,
                        @Email,
                        @Telefone,
                        @DataMatricula,
                        @Ativo);
                ", aluno);
        }

        public async Task DeletarAluno(long id)
        {
            await _connectionFactory.CreateConnection()
                .ExecuteAsync(
                @"
                 DELETE FROM Aluno
                    WHERE Id = @Id
                ", new { Id = id });
        }

        public Task<Aluno> ObterAluno(long id)
        {
            return _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<Aluno>(
                @"
                 SELECT * FROM Aluno
                    WHERE Id = @Id
                ", new { Id = id });
        }

        public Task<IEnumerable<Aluno>> ObterTodosAlunos()
        {
            return _connectionFactory.CreateConnection()
                 .QueryAsync<Aluno>(
                 @"
                 SELECT Id,Nome, Sobrenome FROM Aluno
                ");
        }
    }
}
