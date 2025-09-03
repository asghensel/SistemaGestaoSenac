using Senac.GestaoEscolar.Domain.Dtos.Request.Matriculas;
using Senac.GestaoEscolar.Domain.Dtos.Response.Matriculas;
using Senac.GestaoEscolar.Domain.Models;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;
using Senac.GestaoEscolar.Domain.Repositories.Matriculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Services.Matriculas
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ICursoRepository _cursoRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, IAlunoRepository alunoRepository, ICursoRepository cursoRepository)
        {
            _matriculaRepository = matriculaRepository;
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<IEnumerable<MatriculaResponse>> ObterCursosPorAlunoIdAsync(long alunoId)
        {
            var matriculas = await _matriculaRepository.ObterCursosPorAlunoIdAsync(alunoId);
            return matriculas.Select(m => new MatriculaResponse { CursoId = m.CursoId, DataMatricula = m.DataMatricula });
        }

        public async Task MatricularAsync(MatricularRequest request)
        {
            // Validações
            var aluno = await _alunoRepository.ObterAluno(request.AlunoId);
            if (aluno == null) throw new Exception("Aluno não encontrado.");

            var curso = await _cursoRepository.ObterCurso(request.CursoId);
            if (curso == null) throw new Exception("Curso não encontrado.");

            var matriculaExistente = await _matriculaRepository.ObterPorAlunoECursoAsync(request.AlunoId, request.CursoId);
            if (matriculaExistente != null) throw new Exception("Aluno já está matriculado neste curso.");

            // Cria a nova matrícula
            var novaMatricula = new Matricula
            {
                AlunoId = request.AlunoId,
                CursoId = request.CursoId,
                DataMatricula = DateTime.UtcNow
            };

            await _matriculaRepository.MatricularAsync(novaMatricula);
        }
    }
}