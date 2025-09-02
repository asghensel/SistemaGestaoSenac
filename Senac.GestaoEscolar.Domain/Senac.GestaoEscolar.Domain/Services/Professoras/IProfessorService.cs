using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Professores;

namespace Senac.GestaoEscolar.Domain.Services.Professoras
{
    public interface IProfessorService
    {
        Task AtualizarProfessor(long id, AtualizarProfessorRequest atualizarProfessorRequest);
        Task<CadastrarAlunoResponse> CadastrarProfessor(CadastrarProfessorRequest cadastrarProfessorRequest);
        Task DeletarProfessor(long id);
        Task<DetalheProfessor> ObterProfessor(long id);
        Task<IEnumerable<TodosProfessores>> ObterTodosProfessores();
    }
}
