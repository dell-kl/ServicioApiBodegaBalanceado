using System.Collections.ObjectModel;

namespace Domain.DTO.RequestDto
{
    public class RawMaterialDetailsRequestDto
    {
        public double KgTotal { set; get; }
        public int TotalCompras { set; get; }
        public decimal UltimaCompra { set; get; }

        public IEnumerable<KgMonitoringRequestDto> KgSeguimiento { set; get; } = new List<KgMonitoringRequestDto>();

        public IEnumerable<DataImage> imagenes { set; get; } = new List<DataImage>();
    }


    public class DataImage
    {
        public string Identificador { set; get; } = null!;
        public string Url { set; get; } = null!;
        public bool Estado { set; get; } = false;
    }
}
