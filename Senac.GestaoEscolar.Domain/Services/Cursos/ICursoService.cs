
using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Cursos;

namespace Senac.GestaoEscolar.Domain.Services.Cursos
{
    public interface ICursoService
    {
        Task AtualizarCurso(long id, AtualizarCursoRequest atualizarCursoRequest);
        Task<CadastrarCursoResponse> CadastrarCurso(CadastrarCursoRequest cadastrarCursoRequest);
        Task DeletarCurso(long id);
        Task<DetalheCurso> ObterCurso(long id);
        Task<IEnumerable<TodosCursos>> ObterTodosCursos();
    }
}
