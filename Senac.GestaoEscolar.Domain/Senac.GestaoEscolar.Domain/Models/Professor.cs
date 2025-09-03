using System;

namespace Senac.GestaoEscolar.Domain.Models
{
    public enum Formacao
    {
        EnsinoMedio,
        EnsinoTecnico,
        Graduado,
        PosGraduado,
        Mestrado,
        Doutorado
    }

    public class Professor
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Inicializado
        public string Sobrenome { get; set; } = string.Empty; // Inicializado
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; } = string.Empty; // Inicializado
        public string Telefone { get; set; } = string.Empty; // Inicializado

        public Formacao Formacao { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Ativo { get; set; }
    }
}