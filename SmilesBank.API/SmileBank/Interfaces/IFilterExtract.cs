namespace SmileBank.Interfaces
{
    public class IFilterExtract
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public bool type { get; set; }
        public string status { get; set; }
    }
}
