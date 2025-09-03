using System;

namespace Senac.GestaoEscolar.Domain.Dtos.Request.Professores
{
    public class CadastrarProfessorRequest
    {
        public string Nome { get; set; } = string.Empty; // Inicializado
        public string Sobrenome { get; set; } = string.Empty; // Inicializado
        public string Email { get; set; } = string.Empty; // Inicializado
        public string Telefone { get; set; } = string.Empty; // Inicializado
        public bool Ativo { get; set; }
        public DateTime DataContratacao { get; set; }
        public string Formacao { get; set; } = string.Empty; // Inicializado
        public DateTime DataNascimento { get; set; }
    }
}