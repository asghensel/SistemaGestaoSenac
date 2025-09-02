using Senac.GestaoEscolar.Domain.Dtos.Request.Matriculas;
using Senac.GestaoEscolar.Domain.Dtos.Response.Matriculas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Matriculas
{
    public interface IMatriculaService
    {
        Task<IEnumerable<MatriculaResponse>> ObterCursosPorAlunoIdAsync(long alunoId);
        Task MatricularAsync(MatricularRequest request);
    }
}