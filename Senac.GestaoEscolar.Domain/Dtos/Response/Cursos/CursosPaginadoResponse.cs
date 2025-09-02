using System.Collections.Generic;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Cursos
{
    public class CursosPaginadoResponse
    {
        public List<TodosCursos> Cursos { get; set; } = new List<TodosCursos>();
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}