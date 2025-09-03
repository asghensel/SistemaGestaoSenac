using System;

namespace Senac.GestaoEscolar.Domain.Models
{
    public class Matricula
    {
        public long AlunoId { get; set; }
        public long CursoId { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}