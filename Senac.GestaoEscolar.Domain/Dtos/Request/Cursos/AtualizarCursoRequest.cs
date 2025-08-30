namespace Senac.GestaoEscolar.Domain.Dtos.Request.Cursos
{
    public class AtualizarCursoRequest
    {
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public int Valor { get; set; }
        public int CargaHoraria { get; set; }
        public bool Ativo { get; set; }
        public long ProfessorId { get; set; }
    }
}
