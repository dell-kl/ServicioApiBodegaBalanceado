namespace Domain.DTO.RequestDto
{
    public class KgMonitoringRequestDto
    {
        public string Guid { set; get; } = "";
        public int Cantidad { set; get; } = 0;
        public double Kg { set; get; } = 0.0d;
        public decimal Precio { set; get; } = 0.0m;
        public double KgTotal { set; get; } = 0.0d;
        public decimal PrecioTotal { set; get; } = 0.0m;
        public DateTime FechaCreacion { set; get; } = DateTime.Now;
        public DateTime FechaActualizacion { set; get; } = DateTime.Now;
    }
}
