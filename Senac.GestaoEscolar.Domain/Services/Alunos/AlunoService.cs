using Senac.GestaoEscolar.Domain.Dtos.Request.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;

namespace Senac.GestaoEscolar.Domain.Services.Alunos
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task AtualizarAluno(long id, AtualizarAlunoRequest atualizarAlunoRequest)
        {
            var aluno = await _alunoRepository.ObterAluno(id);
            ValidarSeExiste(aluno, id);

            aluno.Email = atualizarAlunoRequest.Email;
            aluno.Telefone = atualizarAlunoRequest.Telefone;
            aluno.Ativo = atualizarAlunoRequest.Ativo;

            await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task<CadastrarAlunoResponse> CadastrarAluno(CadastrarAlunoRequest cadastrarAlunoRequest)
        {
            var aluno = new Models.Aluno
            {
                Nome = cadastrarAlunoRequest.Nome,
                Sobrenome = cadastrarAlunoRequest.Sobrenome,
                DataDeNascimento = cadastrarAlunoRequest.DataDeNascimento,
                Email = cadastrarAlunoRequest.Email,
                Telefone = cadastrarAlunoRequest.Telefone,
                DataMatricula = DateTime.UtcNow,
                Ativo = true
            };

            long id = await _alunoRepository.CadastrarAluno(aluno);

            return new CadastrarAlunoResponse { Id = id };
        }

        public async Task DeletarAluno(long id)
        {
            var aluno = _alunoRepository.ObterAluno(id);
            if (aluno == null)
            {
                throw new KeyNotFoundException($"Aluno com ID {id} não encontrado.");
            }
            await _alunoRepository.DeletarAluno(id);
        }

        public async Task<DetalheAluno> ObterAluno(long id)
        {
            var aluno = await _alunoRepository.ObterAluno(id);
            if (aluno == null)
            {
                throw new KeyNotFoundException($"Aluno com ID {id} não encontrado.");
            }
            var alunoResponse = new DetalheAluno
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Sobrenome = aluno.Sobrenome,
                DataDeNascimento = aluno.DataDeNascimento,
                Email = aluno.Email,
                Telefone = aluno.Telefone,
                DataMatricula = aluno.DataMatricula,
                Ativo = aluno.Ativo
            };
            return alunoResponse;
        }

        public async Task<IEnumerable<TodosAlunos>> ObterTodosAlunos()
        {
            var alunos = await _alunoRepository.ObterTodosAlunos();
            var alunosResponse = alunos.Select(a => new TodosAlunos
            {
                Id = a.Id,
                Nome = a.Nome,
                Sobrenome = a.Sobrenome
            });
            return alunosResponse;
        }

        private void ValidarSeExiste(Aluno aluno, long id)
        {
            if (aluno == null)
            {
                throw new Exception($"Aluno com ID {id} não encontrado.");
            }
        }
    }
}
