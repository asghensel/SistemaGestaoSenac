namespace Senac.GestaoEscolar.Domain.Dtos.Request.Cursos
{
    public class CadastrarCursoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; } 
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public int CargaHoraria { get; set; }
        public bool Ativo { get; set; } 
        public long ProfessorId { get; set; } 
    }
}
