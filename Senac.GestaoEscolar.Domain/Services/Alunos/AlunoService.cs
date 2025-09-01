using Senac.GestaoEscolar.Domain.Dtos.Request.Alunos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Dtos.Response.Alunos;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Alunos
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        // MÉTODO ATUALIZADO PARA LIDAR COM PAGINAÇÃO
        public async Task<AlunosPaginadoResponse> ObterTodosAlunos(int pagina, int limite)
        {
            // A chamada agora passa os parâmetros para o repositório
            var (alunos, totalDeRegistros) = await _alunoRepository.ObterTodosAlunos(pagina, limite);

            var alunosResponse = alunos.Select(a => new TodosAlunos
            {
                Id = a.Id,
                Nome = a.Nome,
                Sobrenome = a.Sobrenome
            }).ToList();

            var totalDePaginas = (int)Math.Ceiling((double)totalDeRegistros / limite);

            return new AlunosPaginadoResponse
            {
                Alunos = alunosResponse,
                PaginaAtual = pagina,
                TotalDePaginas = totalDePaginas
            };
        }

        public async Task<DetalheAluno> ObterAluno(long id)
        {
            var aluno = await _alunoRepository.ObterAluno(id);
            ValidarSeExiste(aluno, id);

            return new DetalheAluno
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
        }

        public async Task<CadastrarAlunoResponse> CadastrarAluno(CadastrarAlunoRequest cadastrarAlunoRequest)
        {
            var aluno = new Aluno
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

        public async Task AtualizarAluno(long id, AtualizarAlunoRequest atualizarAlunoRequest)
        {
            var aluno = await _alunoRepository.ObterAluno(id);
            ValidarSeExiste(aluno, id);

            aluno.Email = atualizarAlunoRequest.Email;
            aluno.Telefone = atualizarAlunoRequest.Telefone;
            aluno.Ativo = atualizarAlunoRequest.Ativo;

            await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task DeletarAluno(long id)
        {
            var aluno = await _alunoRepository.ObterAluno(id);
            ValidarSeExiste(aluno, id);

            if (aluno.Ativo)
            {
                throw new Exception($"Aluno com ID {id} não pode ser deletado pois está ativo.");
            }

            await _alunoRepository.DeletarAluno(id);
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