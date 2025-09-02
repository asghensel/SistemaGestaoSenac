using System.Collections.Generic;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Professores
{
    public class ProfessoresPaginadoResponse
    {
        public List<TodosProfessores> Professores { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}