namespace Worker.Kafka
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string Topic { get; set; } = "lancamentos-consolidar";
        public string GroupId { get; set; } = "consolidado-worker-group";
    }
}
