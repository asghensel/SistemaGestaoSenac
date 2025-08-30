using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Dtos.Request.Professores
{
    public class AtualizarProfessorRequest
    {
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string Formacao { get; set; }
    }
}
