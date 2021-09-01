namespace Domain.Service.Entities
{
    public class ProcessEntity : Entity
    {
        private string sName = string.Empty;
        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

        private string sDetail = string.Empty;
        public string Detail
        {
            get { return sDetail; }
            set { sDetail = value; }
        }
    }
}
