using Senac.GestaoEscolar.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Repositories.Professores
{
    public interface IProfessorRepository
    {
        Task<(IEnumerable<Professor>, int)> ObterTodosProfessores(int pagina, int limite);
        Task<Professor> ObterProfessorPorId(long id);
        Task<long> CadastrarProfessor(Professor professor);
        Task AtualizarProfessor(Professor professor);
        Task DeletarProfessor(long id);
    }
}