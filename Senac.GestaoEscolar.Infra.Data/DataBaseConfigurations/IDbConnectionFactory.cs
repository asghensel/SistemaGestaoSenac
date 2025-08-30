using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
