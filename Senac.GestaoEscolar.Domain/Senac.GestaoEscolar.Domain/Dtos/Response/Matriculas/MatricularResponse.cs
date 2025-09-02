using System;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Matriculas
{
    public class MatriculaResponse
    {
        public long CursoId { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}