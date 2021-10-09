namespace Domain.Entities
{
    public class ProcessEntity : ProcessParam
    {               
        public int CategoryProcessesId { get; set; }

        public CategoryProcesses Category { get; set; }

        public ProcessEntity(string name, int cycleTime) : base (name, cycleTime)
        {

        }
        public ProcessEntity()
        {

        }
    }
}
