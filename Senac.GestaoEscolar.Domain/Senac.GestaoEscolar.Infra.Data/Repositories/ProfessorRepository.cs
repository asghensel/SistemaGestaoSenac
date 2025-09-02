using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Professores;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public ProfessorRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory; 
        }
        public async Task AtualizarProfessor(Professor professor)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync(
                @"
                 UPDATE Professor
                    SET 
                        email = @Email,
                        telefone = @Telefone,
                        Formacao = @Formacao,
                        ativo = @Ativo
                    WHERE Id = @Id
                    ", professor);
        }

        public async Task<long> CadastrarProfessor(Professor professor)
        {
            return await _connectionFactory.CreateConnection()
                .QuerySingleAsync<long>(

             @"
                INSERT INTO Professor
                    (nome,
                    sobrenome,
                    dataDeNascimento,
                    email,
                    telefone,
                    formacao,
                    dataContratacao,
                    ativo)
                    OUTPUT INSERTED.Id
                VALUES 
                    (@Nome,
                    @Sobrenome,
                    @DataDeNascimento,
                    @Email,
                    @Telefone,
                    @Formacao,
                    @DataContratacao,
                    @Ativo);
                ", professor);


        }

        public async Task DeletarProfessor(long id)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync(
                @"
                DELETE FROM Professor
                WHERE Id = @Id
                ", new { Id = id });
        }

        public async Task<Professor> ObterProfessorPorId(long id)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<Professor>(
                @"
                SELECT 
                    p.Id,
                    p.Nome, 
                    p.Sobrenome, 
                    p.DataDeNascimento,
                    p.Email,
                    p.Telefone,
                    p.Formacao,
                    p.DataContratacao,
                    p.Ativo
                FROM 
                    Professor p
                INNER JOIN 
                    Formacao f ON f.id  = p.Formacao
                WHERE 
                    p.Id = @Id
                ", new { Id = id });
        }

        public Task<IEnumerable<Professor>> ObterTodosProfessores()
        {
            return _connectionFactory.CreateConnection()
                .QueryAsync<Professor>(
                @"
                SELECT 
                    Id, Nome, Sobrenome
                FROM 
                    Professor
                ");
        }
    }
}
