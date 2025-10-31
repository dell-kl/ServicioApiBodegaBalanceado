using System.Collections.ObjectModel;
using ServicioApiBodegaBalanceado.Domain.DTO;

namespace Domain.DTO.RequestDto
{
    public class RawMaterialDetailsRequestDto
    {
        public double KgTotal { set; get; }
        public int TotalCompras { set; get; }
        public decimal UltimaCompra { set; get; }

        public IEnumerable<KgMonitoringRequestDto> KgSeguimiento { set; get; } = new List<KgMonitoringRequestDto>();

        public IEnumerable<DataImageDto> imagenes { set; get; } = new List<DataImageDto>();
    }

}
