using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Interfaces
{
    public interface IAccess
    {
        string GetConnection(string ConnectionString);        
    }
}
