namespace SmileBank.Interfaces
{
    public class IUpdate
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
    }
}
