using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Cursos;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Cursos
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<CursosPaginadoResponse> ObterTodosCursos(int pagina, int limite)
        {
            var (cursos, totalDeRegistros) = await _cursoRepository.ObterTodosCursos(pagina, limite);

            var cursosResponse = cursos.Select(c => new TodosCursos
            {
                Id = c.Id,
                Nome = c.Nome,
                CargaHoraria = c.CargaHoraria
            }).ToList();

            var totalDePaginas = (int)Math.Ceiling((double)totalDeRegistros / limite);

            return new CursosPaginadoResponse
            {
                Cursos = cursosResponse,
                PaginaAtual = pagina,
                TotalDePaginas = totalDePaginas
            };
        }

        public async Task<DetalheCurso> ObterCurso(long id)
        {
            var curso = await _cursoRepository.ObterCurso(id);
            ValidarSeExiste(curso, id);

            var cursoResponse = new DetalheCurso
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                Categoria = curso.Categoria.ToString(),
                Valor = curso.Valor,
                CargaHoraria = curso.CargaHoraria,
                Ativo = curso.Ativo,
                ProfessorId = curso.ProfessorId
            };
            return cursoResponse;
        }

        public async Task<CadastrarCursoResponse> CadastrarCurso(CadastrarCursoRequest cadastrarCursoRequest)
        {
            bool isCategoriaValida = Enum.TryParse(cadastrarCursoRequest.Categoria, ignoreCase: true, out Categoria categoria);
            if (!isCategoriaValida)
            {
                throw new Exception($"Categoria '{cadastrarCursoRequest.Categoria}' inválida.");
            }

            var curso = new Curso
            {
                Nome = cadastrarCursoRequest.Nome,
                Descricao = cadastrarCursoRequest.Descricao,
                Categoria = categoria,
                Valor = cadastrarCursoRequest.Valor,
                CargaHoraria = cadastrarCursoRequest.CargaHoraria,
                ProfessorId = cadastrarCursoRequest.ProfessorId,
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };
            long novoId = await _cursoRepository.CadastrarCurso(curso);
            return new CadastrarCursoResponse { Id = novoId };
        }

        public async Task AtualizarCurso(long id, AtualizarCursoRequest atualizarCursoRequest)
        {
            bool isCategoriaValida = Enum.TryParse(atualizarCursoRequest.Categoria, ignoreCase: true, out Categoria categoria);
            if (!isCategoriaValida)
            {
                throw new Exception($"Categoria '{atualizarCursoRequest.Categoria}' inválida.");
            }

            var curso = await _cursoRepository.ObterCurso(id);
            ValidarSeExiste(curso, id);

            curso.Descricao = atualizarCursoRequest.Descricao;
            curso.Categoria = categoria;
            curso.Valor = atualizarCursoRequest.Valor;
            curso.CargaHoraria = atualizarCursoRequest.CargaHoraria;
            curso.Ativo = atualizarCursoRequest.Ativo;
            curso.ProfessorId = atualizarCursoRequest.ProfessorId;

            await _cursoRepository.AtualizarCurso(curso);
        }

        public async Task DeletarCurso(long id)
        {
            var curso = await _cursoRepository.ObterCurso(id);
            ValidarSeExiste(curso, id);

            // Regra de negócio: não permitir deletar um curso que está ativo.
            if (curso.Ativo == true)
            {
                throw new Exception("Não é possível deletar um curso ativo. Por favor, inative-o primeiro.");
            }
            await _cursoRepository.DeletarCurso(id);
        }

        private void ValidarSeExiste(Curso curso, long id)
        {
            if (curso == null)
            {
                throw new Exception($"Curso com ID {id} não encontrado.");
            }
        }
    }
}