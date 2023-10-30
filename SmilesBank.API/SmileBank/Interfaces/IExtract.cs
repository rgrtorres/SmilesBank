namespace SmileBank.Interfaces
{
    public class IExtract
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Type { get; set; } /* Type: 0 System 1 Avulso */
        public string Status { get; set; } /* Válido/Cancelado */

        public IExtract(string description, double amount, bool type, string status)
        {
            Id = Guid.NewGuid();
            Description = description;
            Date = DateTime.Now;
            Amount = amount;
            Type = type;
            Status = status;
        }
    }
}
