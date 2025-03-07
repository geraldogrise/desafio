namespace Worker.Kafka.Entity
{
    public class Consolidado
    {

        public Guid Id { get; set; }
        public string EventId { get; set; }
        public DateTime DataConsolidado { get; set; }
        public decimal ValorDebito { get; set; }
        public decimal ValorCredito { get; set; }
        public string Token { get; set; }
    }
}
