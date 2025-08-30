using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Professores
{
    public class DetalheProfessor
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataContratacao { get; set; }
        public string Formacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public DetalheProfessor()
        {
            DataContratacao = DateTime.Now;
            Ativo = true;
        }
    }
}
