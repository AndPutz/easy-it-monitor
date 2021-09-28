using Domain.Service.Interfaces;
using System.Configuration;

namespace Infra
{
    public class Access : IAccess
    {        
        public string GetConnection(string ConnectionString = "ConnectionDB")
        {
            return ConfigurationManager.ConnectionStrings["ConnectionDB"].ToString();
        }
    }
}
