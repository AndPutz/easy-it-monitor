namespace Domain.Entities
{
    public sealed class ServiceEntity : ParamEntity
    {
        public int CategoryServicesId { get; set; }

        public CategoryServices Category { get; set; }

        public ServiceEntity(string name, int cycleTime) : base (name, cycleTime)
        {

        }

        public ServiceEntity()
        {

        }
    }
}
