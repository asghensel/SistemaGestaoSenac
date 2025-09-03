using Senac.GestaoEscolar.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Repositories.Cursos
{
    public interface ICursoRepository
    {
        Task<(IEnumerable<Curso>, int)> ObterTodosCursos(int pagina, int limite);
        Task<Curso> ObterCurso(long id);
        Task<long> CadastrarCurso(Curso curso);
        Task AtualizarCurso(Curso curso);
        Task DeletarCurso(long id);
    }
}