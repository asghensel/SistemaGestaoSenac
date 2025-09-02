using Senac.GestaoEscolar.Domain.Dtos.Request.Professores;
using Senac.GestaoEscolar.Domain.Dtos.Response.Professores;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Professores;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Professores
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<ProfessoresPaginadoResponse> ObterTodosProfessores(int pagina, int limite)
        {
            var (professores, totalDeRegistros) = await _professorRepository.ObterTodosProfessores(pagina, limite);

            var professoresResponse = professores.Select(p => new TodosProfessores
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome
            }).ToList();

            var totalDePaginas = (int)Math.Ceiling((double)totalDeRegistros / limite);

            return new ProfessoresPaginadoResponse
            {
                Professores = professoresResponse,
                PaginaAtual = pagina,
                TotalDePaginas = totalDePaginas
            };
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

        public async Task<CadastrarProfessorResponse> CadastrarProfessor(CadastrarProfessorRequest cadastrarProfessorRequest)
        {
            bool isFormacaoValida = Enum.TryParse(cadastrarProfessorRequest.Formacao, ignoreCase: true, out Formacao formacao);
            if (!isFormacaoValida)
            {
                throw new Exception($"Formação '{cadastrarProfessorRequest.Formacao}' inválida.");
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

            long novoId = await _professorRepository.CadastrarProfessor(professor);
            return new CadastrarProfessorResponse { Id = novoId };
        }

        public async Task AtualizarProfessor(long id, AtualizarProfessorRequest atualizarProfessorRequest)
        {
            bool isFormacaoValida = Enum.TryParse(atualizarProfessorRequest.Formacao, ignoreCase: true, out Formacao formacao);
            if (!isFormacaoValida)
            {
                throw new Exception($"Formação '{atualizarProfessorRequest.Formacao}' inválida.");
            }

            var professor = await _professorRepository.ObterProfessorPorId(id);
            ValidarSeExiste(professor, id);

            professor.Email = atualizarProfessorRequest.Email;
            professor.Telefone = atualizarProfessorRequest.Telefone;
            professor.Ativo = atualizarProfessorRequest.Ativo;
            professor.Formacao = formacao;

            await _professorRepository.AtualizarProfessor(professor);
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

        private void ValidarSeExiste(Professor professor, long id)
        {
            if (professor == null)
            {
                throw new Exception($"Professor com ID {id} não encontrado.");
            }
        }
    }
}