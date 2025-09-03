using System;

namespace Senac.GestaoEscolar.Domain.Dtos.Request.Cursos
{
    public class CadastrarCursoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int CargaHoraria { get; set; }
        public long ProfessorId { get; set; }
    }
}