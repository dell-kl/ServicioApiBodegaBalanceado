using System.ComponentModel.DataAnnotations;


namespace Domain.DTO
{
    public class RawMaterialDto
    {
        public string? id_dto { set; get; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Es necesario introducir un nombre del producto comprado")]
        public string nombre_dto { set; get; } = null!;

        public IEnumerable<KGMonitoringDto> KgMonitoringDtos { set; get; } = new List<KGMonitoringDto>();
    }
}
