using Senac.GestaoEscolar.Domain.Dtos.Request.Cursos;
using Senac.GestaoEscolar.Domain.Dtos.Response.Cursos;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;

namespace Senac.GestaoEscolar.Domain.Services.Cursos
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task AtualizarCurso(long id, AtualizarCursoRequest atualizarCursoRequest)
        {
            bool isCategoriaValida = Enum.TryParse(atualizarCursoRequest.Categoria, ignoreCase:true, out Categoria categoria);
            if (isCategoriaValida)
            {
                ValidarSeCategoriaValida(categoria);
            }
            
            var curso = await _cursoRepository.ObterCurso(id);
            ValidarSeExiste(curso, id);

            curso.Descricao = atualizarCursoRequest.Descricao;
            curso.Categoria = categoria;
            curso.Valor = atualizarCursoRequest.Valor;
            curso.CargaHoraria = atualizarCursoRequest.CargaHoraria;
            curso.Ativo = atualizarCursoRequest.Ativo;
            curso.ProfessorId = atualizarCursoRequest.ProfessorId;
        }

        public async Task<CadastrarCursoResponse> CadastrarCurso(CadastrarCursoRequest cadastrarCursoRequest)
        {
            bool isCategoriaValida = Enum.TryParse(cadastrarCursoRequest.Categoria, ignoreCase: true, out Categoria categoria);
            if (isCategoriaValida)
            {
                ValidarSeCategoriaValida(categoria);
            }
            var curso = new Curso
            {
                Nome = cadastrarCursoRequest.Nome,
                Descricao = cadastrarCursoRequest.Descricao,
                Categoria = categoria,
                Valor = cadastrarCursoRequest.Valor,
                DataCriacao = cadastrarCursoRequest.DataCriacao,
                CargaHoraria = cadastrarCursoRequest.CargaHoraria,
                Ativo = cadastrarCursoRequest.Ativo,
                ProfessorId = cadastrarCursoRequest.ProfessorId
            };
            long id = await _cursoRepository.CadastrarCurso(curso);
            return new CadastrarCursoResponse { Id = id };
        }

        public async Task DeletarCurso(long id)
        {
            var curso = await _cursoRepository.ObterCurso(id);
            ValidarSeExiste(curso, id);
            if (curso.Ativo == true)
            {
                throw new Exception("Não é possível deletar um curso ativo.");
            }
            await _cursoRepository.DeletarCurso(id);
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

        public async Task<IEnumerable<TodosCursos>> ObterTodosCursos()
        {
            var cursos = await _cursoRepository.ObterTodosCursos();
            var cursosResponse = cursos.Select(c => new TodosCursos
            {
                Id = c.Id,
                Nome = c.Nome,
                Categoria = c.Categoria.ToString()
            });
            return cursosResponse;
        }

        private void ValidarSeExiste(Curso curso, long id)
        {
            if (curso == null)
            {
                throw new Exception($"Curso com ID {id} não encontrado.");
            }
        }

        private void ValidarSeCategoriaValida(Categoria categoria)
        {
            if (!Enum.IsDefined(typeof(Categoria), categoria))
            {
                throw new Exception($"Categoria {categoria} inválida.");
            }
        }

    }
}
