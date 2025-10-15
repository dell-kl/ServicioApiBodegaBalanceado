using System.Collections.ObjectModel;

namespace Domain.DTO.RequestDto
{
    public class RawMaterialDetailsRequestDto
    {
        public double KgTotal { set; get; }
        public int TotalCompras { set; get; }
        public decimal UltimaCompra { set; get; }

        public IEnumerable<KgMonitoringRequestDto> KgSeguimiento { set; get; } = new List<KgMonitoringRequestDto>();

        public IEnumerable<string> imagenes { set; get; } = new List<string>();
    }
}
