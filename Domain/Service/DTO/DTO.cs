using Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.DTO
{
    public class DTO
    {
        public OleDbConnection DbConnection { get; set; }

        public bool IsConnectionAvaible()
        {
            return DbConnection != null && DbConnection.State == ConnectionState.Open;
        }

        public void VerifyOpenDB()
        {
            try
            {
                if (DbConnection == null && !String.IsNullOrEmpty(Access.Connection))
                {
                    DbConnection = new OleDbConnection(Access.Connection);

                    DbConnection.Open();
                }
                else if (DbConnection.State != ConnectionState.Open)
                {
                    DbConnection.Open();
                }
            }
            catch
            {
            }
        }
    }
}
