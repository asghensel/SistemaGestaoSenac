using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public CursoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AtualizarCurso( Curso curso)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync(
                @"
                 UPDATE Curso
                    SET 
                        
                        descricao = @Descricao,
                        cargaHoraria = @CargaHoraria,
                        valor = @Valor,
                        professorId = @ProfessorId,
                        ativo = @Ativo
                    WHERE Id = @Id
                    ", curso);
        }

        public async Task<long> CadastrarCurso(Curso curso)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<long>(
                @"
                INSERT INTO Curso ( 
                    nome, 
                    descricao,
                    dataCriacao,
                    cargaHoraria,
                    categoria,
                    valor,
                    ativo,  
                    professorId
                    )
                    OUTPUT INSERTED.Id      
                VALUES (
                    @Nome,
                    @Descricao,
                    @DataCriacao,
                    @CargaHoraria,
                    @Categoria,
                    @Valor,
                    @Ativo,
                    @ProfessorId    
                    );
                ", curso);
        }

        public async Task DeletarCurso(long id)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<long>(
                @"
                DELETE FROM Curso
                WHERE Id = @Id;
                ", new { Id = id });
        }

        public async Task<Curso> ObterCurso(long id)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<Curso>(
                @"
                SELECT 
                   
                        c.Id,
                        c.Nome,
                        c.Descricao,
                        c.DataCriacao,
                        c.CargaHoraria,
                        c.Categoria ,
                        c.Valor,
                        c.Ativo,
                        c.ProfessorId
                FROM Curso c
                INNER JOIN Professor p ON p.Id = c.ProfessorId
                INNER JOIN Categoria n ON n.Id = c.Categoria
                WHERE Id = @Id;
                ", new { Id = id });
        }

        public Task<IEnumerable<Curso>> ObterTodosCursos()
        {
            return _connectionFactory.CreateConnection()
                .QueryAsync<Curso>(
                @"
                SELECT 
                        c.Id,
                        c.Nome,
                        c.Categoria 
                FROM Curso c
                INNER JOIN Categoria n ON n.Id = c.Categoria;
                ");
        }
    }
}
