using ServicioApiBodegaBalanceado.Domain.DTO;

namespace Domain.DTO.RequestDto
{
    public class CatalogProductRequestDto
    {
        public string identificador { set; get; } = Guid.NewGuid().ToString();
        public string nombreProducto { set; get; } = null!;
        public string rutaImagen { set; get; } = null!;
        public DateTime fechaCreacion { set; get; } = DateTime.Now;
        public int numeroCategorias { set; get; } = 0;
        public int totalSacosCatalogo { set; get; } = 0;
    }

    //otro DTO pero relacionado con lo que es nuestro modelo de Domain.DataCatalogProduct..
    /*
    DataProduct es la clase de la cual se va a heredar debido a que sus propiedades son de gran utilidad dentro de este DTO.
    */
    public class DataCatalogProductoRequestDto : DataProduct
    {
        public string guid { set; get; } = Guid.NewGuid().ToString();

        public DateTime fechaCreacion { set; get; } = DateTime.Now;
        public DateTime fechaActualizacion { set; get; } = DateTime.Now;
    }
}
