namespace Domain.DTO;

public class ManufacturedDto
{
    public string identificadorDataCatalogProduct { set; get; } = null!;
    public string identificadorProduccion { set; get; } = null!;
    public int cantidadCostales { set; get; } = 1;
}