using Senac.GestaoEscolar.Domain.Models;

namespace Senac.GestaoEscolar.Domain.Repositories.Alunos
{
    public interface IAlunoRepository
    {
        Task AtualizarAluno( Aluno aluno);
        Task<long> CadastrarAluno(Aluno aluno);
        Task DeletarAluno(long id);
        Task<Aluno> ObterAluno(long id);
        Task<IEnumerable<Aluno>> ObterTodosAlunos();
    }
}
