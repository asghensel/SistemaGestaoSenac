namespace Senac.GestaoEscolar.Domain.Dtos.Request.Alunos
{
    public class AtualizarAlunoRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}