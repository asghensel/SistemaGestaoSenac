namespace Senac.GestaoEscolar.Domain.Dtos.Response.Cursos
{
    public class DetalheCurso
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public int CargaHoraria { get; set; }
        public bool Ativo { get; set; }
        public long ProfessorId { get; set; }

    }
}
