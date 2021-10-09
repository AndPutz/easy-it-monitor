
namespace Domain.Entities
{
    public class ProcessParam : ParamEntity
    {
        public string Detail { get; set; }

        public ProcessParam(string name, int cycleTime) : base(name, cycleTime)
        {
            
        }
        public ProcessParam()
        {

        }
    }
}
