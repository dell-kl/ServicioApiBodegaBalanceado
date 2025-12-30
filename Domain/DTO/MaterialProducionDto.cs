using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class MaterialProducionDto
    {
        public string id_dto { set; get; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Debes ingresar los kilogramos que vas a usar")]
        public double KgUse_dto { set; get; }
    }
}
 