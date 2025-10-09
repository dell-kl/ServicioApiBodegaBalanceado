
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class MaterialProduction
    {
        [Key]
        public int MaterialProduction_id { set; get; }

        public Guid MaterialProduction_guid { set; get; } = Guid.NewGuid();

        public double MaterialProduction_KGUsed { set; get; } = 0.0d;

        public DateTime MaterialProduction_created { set; get; } = DateTime.Now;
        public DateTime MaterialProduction_updated { set; get; } = DateTime.Now;

        [Column("MaterialProduction_IDRawMaterial")]
        public int RawMaterialRawMaterial_id { set; get; }

        [Column("MaterialProduction_IDProduction")]
        public int ProductionProduction_id { set; get; }

        [ForeignKey("RawMaterialRawMaterial_id")]
        public RawMaterial RawMaterial { set; get; } = new RawMaterial();

        [ForeignKey("ProductionProduction_id")]
        public Production? Production { set; get; } = null;
    }
}
