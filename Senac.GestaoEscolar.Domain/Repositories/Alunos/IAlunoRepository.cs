using Senac.GestaoEscolar.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Repositories.Alunos
{
    public interface IAlunoRepository
    {

        Task<(IEnumerable<Aluno>, int)> ObterTodosAlunos(int pagina, int limite);

        Task<Aluno> ObterAluno(long id);
        Task<long> CadastrarAluno(Aluno aluno);
        Task AtualizarAluno(Aluno aluno);
        Task DeletarAluno(long id);
    }
}