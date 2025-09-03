namespace Senac.GestaoEscolar.Domain.Dtos.Response.Cursos
{
    public class TodosCursos
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CargaHoraria { get; set; } 
        public string Categoria { get; set; } = string.Empty;
    }
}