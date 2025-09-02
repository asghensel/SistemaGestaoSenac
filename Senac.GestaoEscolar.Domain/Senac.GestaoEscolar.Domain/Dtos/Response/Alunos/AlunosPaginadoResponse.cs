using System.Collections.Generic;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Alunos
{
    public class AlunosPaginadoResponse
    {
        public List<TodosAlunos> Alunos { get; set; } = new List<TodosAlunos>();
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}