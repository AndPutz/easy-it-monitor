namespace Domain.Entities
{
    public class ProcessEntity : ParamEntity
    {       
        public string Detail { get; set; }

        public int CategoryProcessesId { get; set; }

        public CategoryProcesses Category { get; set; }
    }
}
