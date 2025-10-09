namespace Domain.DTO
{
    public class KGMonitoringDto
    {
        public string? id_dto { set; get; } = Guid.NewGuid().ToString();

        public int cantidad_dto { set; get; }

        public double kg_standard { set; get; }

        public decimal price_dto { set; get; }
    }
}
