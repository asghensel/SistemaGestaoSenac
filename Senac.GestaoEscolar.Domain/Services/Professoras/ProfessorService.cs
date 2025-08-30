using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Professores;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Professores;

namespace Senac.GestaoEscolar.Domain.Services.Professoras
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }
        public async Task AtualizarProfessor(long id, AtualizarProfessorRequest atualizarProfessorRequest)
        {
            bool isFormacaoValida = Enum.TryParse(atualizarProfessorRequest.Formacao, ignoreCase:true,  out Formacao formacao);
            if (!isFormacaoValida)
            {
                throw new Exception($"Formação {atualizarProfessorRequest.Formacao} inválida.");
            }
            
            var professor = await _professorRepository.ObterProfessorPorId(id);
            ValidarSeExiste(professor, id);
            professor.Email = atualizarProfessorRequest.Email;
            professor.Telefone = atualizarProfessorRequest.Telefone;
            professor.Ativo = atualizarProfessorRequest.Ativo;
            professor.Formacao = formacao;

            await _professorRepository.AtualizarProfessor(professor);
        }

        public async Task<CadastrarAlunoResponse> CadastrarProfessor(CadastrarProfessorRequest cadastrarProfessorRequest)
        {
            bool isFormacaoValida = Enum.TryParse(cadastrarProfessorRequest.Formacao, ignoreCase: true, out Formacao formacao);
            if (!isFormacaoValida)
            {
                throw new Exception($"Formação {cadastrarProfessorRequest.Formacao} inválida.");

            }

            var professor = new Professor
            {
                Nome = cadastrarProfessorRequest.Nome,
                Sobrenome = cadastrarProfessorRequest.Sobrenome,
                Email = cadastrarProfessorRequest.Email,
                Telefone = cadastrarProfessorRequest.Telefone,
                Ativo = cadastrarProfessorRequest.Ativo,
                DataContratacao = cadastrarProfessorRequest.DataContratacao,
                Formacao = formacao,
                DataDeNascimento = cadastrarProfessorRequest.DataNascimento
            };
            long id = await _professorRepository.CadastrarProfessor(professor);
            return new CadastrarAlunoResponse { Id = id,};

        }

        public async Task DeletarProfessor(long id)
        {
            var professor = await _professorRepository.ObterProfessorPorId(id);
            ValidarSeExiste(professor, id);
            if (professor.Ativo == true)
            {
                throw new Exception($"Professor com ID {id} não pode ser deletado porque está ativo.");
            }
            await _professorRepository.DeletarProfessor(id);
        }

        public async Task<DetalheProfessor> ObterProfessor(long id)
        {
            var professor = await _professorRepository.ObterProfessorPorId(id);
            ValidarSeExiste(professor, id);
            var professorResponse = new DetalheProfessor
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Sobrenome = professor.Sobrenome,
                Email = professor.Email,
                Telefone = professor.Telefone,
                Ativo = professor.Ativo,
                DataContratacao = professor.DataContratacao,
                Formacao = professor.Formacao.ToString(),
                DataNascimento = professor.DataDeNascimento
            };
            return professorResponse;
        }

        public async Task<IEnumerable<TodosProfessores>> ObterTodosProfessores()
        {
            var professores = await _professorRepository.ObterTodosProfessores();
            var professoresResponse = professores.Select(p => new TodosProfessores
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome,
            });
            return professoresResponse;
        }

        private void ValidarSeExiste(Professor professor, long id)
        {
            if (professor == null)
            {
                throw new Exception($"Professor com ID {id} não encontrado.");
            }
        }


        private void ValidarSeFormacaoValida(Formacao formacao)
        {
            if (!Enum.IsDefined(typeof(Formacao), formacao))
            {
                throw new Exception($"Formação {formacao} inválida.");
            }
        }

    }
}
