namespace Domain.DTO
{
    public class ProductionDto
    {
        public string catalogoIdentificador { set; get; } = null!;

        public int numeroVecesProduccion { set; get; } = 1;
        
        public string nombreProduccion { set; get; } = null!;
        
        public ICollection<MaterialProducionDto> materialesProduccion { set; get; } = new List<MaterialProducionDto>();
    }
}