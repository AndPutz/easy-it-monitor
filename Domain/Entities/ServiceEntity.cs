namespace Domain.Entities
{
    public class ServiceEntity : ParamEntity
    {
        public int CategoryServicesId { get; set; }

        public CategoryServices Category { get; set; }

    }
}
