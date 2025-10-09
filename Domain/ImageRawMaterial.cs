using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ImageRawMaterial
    {
        [Key]
        public int ImageRawMaterial_id { set; get; }
        public Guid ImageRawMaterial_guid { set; get; } = Guid.NewGuid();
        public string ImageRawMaterial_url { set; get; } = null!; 
        public DateTime ImageRawMaterial_fechaCreacion { set; get; } = DateTime.Now;
        public DateTime ImageRawMaterial_fechaActualizacion { set; get; } = DateTime.Now;

        [Column("ImageRawMaterial_IDRawMaterial")]
        public int RawMaterialRawMateria_id { set; get; }

        [ForeignKey("RawMaterialRawMateria_id")]
        public RawMaterial RawMaterial { set; get; } = new RawMaterial();
    }
}
