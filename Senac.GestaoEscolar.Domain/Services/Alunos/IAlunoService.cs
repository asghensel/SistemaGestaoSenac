using Senac.GestaoEscolar.Domain.Dtos.Request.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Alunos
{
    public interface IAlunoService
    {
        // ASSINATURA CORRIGIDA para corresponder ao AlunoService
        Task<AlunosPaginadoResponse> ObterTodosAlunos(int pagina, int limite);

        Task<DetalheAluno> ObterAluno(long id);
        Task<CadastrarAlunoResponse> CadastrarAluno(CadastrarAlunoRequest cadastrarAlunoRequest);
        Task AtualizarAluno(long id, AtualizarAlunoRequest atualizarAlunoRequest);
        Task DeletarAluno(long id);
    }
}