using Senac.GestaoEscolar.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Repositories.Matriculas
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<Matricula>> ObterPorAlunoIdAsync(long alunoId);
        Task<Matricula?> ObterPorAlunoECursoAsync(long alunoId, long cursoId);
        Task MatricularAsync(Matricula matricula);
    }
}