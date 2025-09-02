namespace Senac.GestaoEscolar.Domain.Dtos.Request.Professores
{
    public class AtualizarProfessorRequest
    {
        public string Email { get; set; } = string.Empty; // Inicializado
        public string Telefone { get; set; } = string.Empty; // Inicializado
        public string Formacao { get; set; } = string.Empty; // Inicializado
        public bool Ativo { get; set; }
    }
}