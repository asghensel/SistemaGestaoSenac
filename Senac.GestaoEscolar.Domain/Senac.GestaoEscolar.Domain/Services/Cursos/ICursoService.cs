using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Cursos;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Cursos
{
    public interface ICursoService
    {
        Task<CursosPaginadoResponse> ObterTodosCursos(int pagina, int limite);
        Task<DetalheCurso> ObterCurso(long id);
        Task<CadastrarCursoResponse> CadastrarCurso(CadastrarCursoRequest cadastrarCursoRequest);
        Task AtualizarCurso(long id, AtualizarCursoRequest atualizarCursoRequest);
        Task DeletarCurso(long id);
    }
}