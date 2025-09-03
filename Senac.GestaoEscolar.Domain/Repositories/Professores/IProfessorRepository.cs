using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.GestaoEscolar.Domain.Models;

namespace Senac.GestaoEscolar.Domain.Repositories.Professores
{
    public interface IProfessorRepository
    {
        Task AtualizarProfessor( Professor professor);
        Task<long> CadastrarProfessor(Professor professor);
        Task DeletarProfessor(long id);
        Task<Professor> ObterProfessorPorId(long id);
        Task<IEnumerable<Professor>> ObterTodosProfessores();
    }
}
