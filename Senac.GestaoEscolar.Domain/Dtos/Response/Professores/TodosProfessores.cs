using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Domain.Dtos.Response.Professores
{
    public class TodosProfessores
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Graduacao { get; set; }   
    }
}
