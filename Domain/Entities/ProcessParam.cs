
namespace Domain.Entities
{
    public class ProcessParam : ParamEntity
    {
        public string Detail { get; set; }

        public ProcessParam(string name, int cycleTime, string detail) : base(name, cycleTime)
        {
            Detail = detail;
        }
        public ProcessParam()
        {

        }        

        public void Update(string name, int cycleTime, string detail)
        {
            base.Update(name, cycleTime);

            Detail = detail;
        }

    }
}
