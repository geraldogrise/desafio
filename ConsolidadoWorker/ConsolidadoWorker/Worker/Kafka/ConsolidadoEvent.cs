namespace Worker.Kafka
{
    public class ConsolidadoEvent
    {
        public Guid Id { get; set; }
        public DateTime DataConsolidado { get; set; }
        public decimal ValorDebito { get; set; }
        public decimal ValorCredito { get; set; }
        public decimal SaldoFinal { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
