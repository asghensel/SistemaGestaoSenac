using Senac.GestaoEscolar.Domain.Dtos.Request.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;

namespace Senac.GestaoEscolar.Domain.Services.Alunos
{
    public interface IAlunoService
    {
        Task AtualizarAluno(long id, AtualizarAlunoRequest atualizarAlunoRequest);
        Task<CadastrarAlunoResponse> CadastrarAluno(CadastrarAlunoRequest cadastrarAlunoRequest);
        Task DeletarAluno(long id);
        Task<DetalheAluno> ObterAluno(long id);
        Task<IEnumerable<TodosAlunos>> ObterTodosAlunos();
    }
}
