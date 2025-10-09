using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class Manufacturing
    {
        [Required(ErrorMessage = "Debes escoger una producto de Materia Prima para empezar la produccion")]
        public string? guid_storeRawMaterial { set; get; }

        [Range(10.00, double.MaxValue, ErrorMessage = "Debes ingresar un valor mayor a 0")]
        public double preparation_kg { set; get; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes ingresar un monto mayor a 0")]
        public int amount { set; get; }

        [Range(2.50, double.MaxValue, ErrorMessage = "Debes ingresar un precio unitario para el producto")]
        public decimal priceUnit { set; get; }

        [Required(ErrorMessage = "Es necesario que ingreses una observacion o recordatorio de tu producto producido")]
        public string observation { set; get; } = "Sin Observaciones";


        //este sera modificado por referencia no sera importante para el envio del json en una 
        //peticion de tipo POST
        public int indice { set; get; } = 0;
    }
}
