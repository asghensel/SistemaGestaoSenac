using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response.Professores;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Professores
{
    public interface IProfessorService
    {
        Task<ProfessoresPaginadoResponse> ObterTodosProfessores(int pagina, int limite);
        Task<DetalheProfessor> ObterProfessor(long id);
        Task<CadastrarProfessorResponse> CadastrarProfessor(CadastrarProfessorRequest cadastrarProfessorRequest);
        Task AtualizarProfessor(long id, AtualizarProfessorRequest atualizarProfessorRequest);
        Task DeletarProfessor(long id);
    }
}