namespace Domain.DTO
{
    public class ProductionDto
    {
        public string catalogoIdentificador { set; get; } = null!;
        public string dataCatalogoIdentificador { set; get; } = null!; // Este atributo corresponde a los Datos de Venta

        public ICollection<MaterialProducionDto> materialesProduccion { set; get; } = new List<MaterialProducionDto>();
    }
}