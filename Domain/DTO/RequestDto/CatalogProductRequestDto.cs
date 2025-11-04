namespace Domain.DTO.RequestDto
{
    public class CatalogProductRequestDto
    {
        public CatalogProductRequestDto() { }

        public string guid { set; get; } = Guid.NewGuid().ToString();
        public string nombreProducto { set; get; } = null!;
        public string rutaImagen { set; get; } = null!;
    }
}
