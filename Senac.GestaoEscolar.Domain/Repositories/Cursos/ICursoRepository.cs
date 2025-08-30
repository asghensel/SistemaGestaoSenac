using Senac.GestaoEscolar.Domain.Models;

namespace Senac.GestaoEscolar.Domain.Repositories.Cursos
{
    public interface ICursoRepository
    {
        Task AtualizarCurso(Curso curso);
        Task<long> CadastrarCurso(Curso curso);
        Task DeletarCurso(long id);
        Task<Curso> ObterCurso(long id);
        Task<IEnumerable<Curso>> ObterTodosCursos();
    }
}
