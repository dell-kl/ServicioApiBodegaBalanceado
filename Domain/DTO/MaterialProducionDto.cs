using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    internal class MaterialProducionDto
    {
        public Guid id_dto { set; get; } = Guid.NewGuid();

        [Required(ErrorMessage = "Debes ingresar los kilogramos que vas a usar")]
        public double KgUse_dto { set; get; }
    }
}
