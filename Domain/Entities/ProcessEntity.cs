using Domain.Validation;

namespace Domain.Entities
{
    public sealed class ProcessEntity : ProcessParam
    {               
        public int CategoryProcessesId { get; set; }

        public CategoryProcesses Category { get; set; }

        public ProcessEntity(string name, int cycleTime, string detail) : base (name, cycleTime, detail)
        {

        }
        /// <summary>
        /// Used by Load XML Param Config
        /// </summary>
        public ProcessEntity()
        {

        }

        public void Update(string name, int cycleTime, int idCategory)
        {
            base.Update(name, cycleTime);

            CategoryProcessesId = idCategory;
        }        
    }
}
