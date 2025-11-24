using ServicioApiBodegaBalanceado.Domain.DTO;

namespace Domain.DTO.RequestDto
{
    public class CatalogProductDetailsRequestDto
    {
        /// <summary>
        /// Número de costales hechos en la última producción.
        /// </summary>
        public int ultimaProduccion { set; get; } = 0;

        /// <summary>
        /// Valor de la última venta realizada.
        /// </summary>
        public decimal ultimaVenta { set; get; } = 0.0m;

        /// <summary>
        /// Número de categorías que hay en total.
        /// </summary>
        public int categorias { set; get; } = 0;

        /// <summary>
        /// Número de costales en total.
        /// </summary>
        public int costalesTotal { set; get; } = 0;

        /// <summary>
        /// Listado de imágenes asociadas al producto.
        /// </summary>
        public IEnumerable<DataImageDto> imagenes { set; get; } = new List<DataImageDto>();
    }
}