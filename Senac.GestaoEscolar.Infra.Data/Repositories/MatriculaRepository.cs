using Dapper;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Matriculas;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Infra.Data.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MatriculaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Matricula>> ObterPorAlunoIdAsync(long alunoId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT AlunoId, CursoId, DataMatricula FROM Matriculas WHERE AlunoId = @AlunoId";
                return await connection.QueryAsync<Matricula>(sql, new { AlunoId = alunoId });
            }
        }

        public async Task<Matricula?> ObterPorAlunoECursoAsync(long alunoId, long cursoId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT * FROM Matriculas WHERE AlunoId = @AlunoId AND CursoId = @CursoId";
                return await connection.QueryFirstOrDefaultAsync<Matricula>(sql, new { AlunoId = alunoId, CursoId = cursoId });
            }
        }

        public async Task MatricularAsync(Matricula matricula)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "INSERT INTO Matriculas (AlunoId, CursoId, DataMatricula) VALUES (@AlunoId, @CursoId, @DataMatricula)";
                await connection.ExecuteAsync(sql, matricula);
            }
        }
    }
}