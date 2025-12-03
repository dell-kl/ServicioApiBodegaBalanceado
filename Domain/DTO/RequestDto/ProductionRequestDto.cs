namespace Domain.DTO.RequestDto
{
    public class ProductionRequestDto
    {
        public string Identificador { set; get; } = "";

        public double KgTotal { set; get; } = 0;

        public int Estado { set; get; } = 1;
        public string NombreProducto { set; get; } = "";

        public DateTime Creado { set; get; } = DateTime.Now;

        public DateTime Actualizado { set; get; } = DateTime.Now;

    }
}