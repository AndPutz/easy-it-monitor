using Domain.Service.Interfaces;
using System;
using System.Data;
using System.Data.OleDb;

namespace Domain.Service.DTO
{
    public class DTO
    {
        public OleDbConnection DbConnection { get; set; }

        public IAccess Access { get; set; }

        public DTO (IAccess access)
        {
            Access = access;
        }

        public bool IsConnectionAvaible()
        {
            return DbConnection != null && DbConnection.State == ConnectionState.Open;
        }
        
        public void VerifyOpenDB()
        {
            try
            {
                if (DbConnection == null && !String.IsNullOrEmpty(Access.GetConnection(null)))
                {
                    DbConnection = new OleDbConnection(Access.GetConnection(null));

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
