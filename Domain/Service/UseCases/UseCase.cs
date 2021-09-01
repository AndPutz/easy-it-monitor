using System;

namespace Domain.Service.UseCases
{
    public class UseCase
    {        
        /// <summary>
        /// Machine Id to register data.
        /// </summary>
        public string HostName 
        { 
            get
            {
                return Environment.MachineName;
            }
        }

        //TODO: remover after Api implementation
        public Int64 IdMachine { get; set; }
    }
}
