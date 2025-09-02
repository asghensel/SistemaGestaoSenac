using System;

namespace Senac.GestaoEscolar.Domain.Dtos.Request.Alunos
{
    public class CadastrarAlunoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        // As propriedades DataMatricula e Ativo foram REMOVIDAS daqui.
    }
}