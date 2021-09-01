namespace Domain.Service.Entities
{
    public class ServiceEntity : Entity
    {
        private string sName = string.Empty;
        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }
    }
}
