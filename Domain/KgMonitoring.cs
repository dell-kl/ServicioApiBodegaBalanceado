using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class KgMonitoring
    {
        [Key]
        public int KgMonitoring_id { set; get; }

        public Guid KgMonitoring_guid { set; get; } = Guid.NewGuid();

        public double KgMonitoring_KGStandard { set; get; } = 0.0d;

        public double KgMonitoring_Total { set; get; } = 0.0d;

        public decimal KgMonitoring_priceUnit { set; get; } = 0.0m;

        public decimal KgMonitoring_priceTotal { set; get; } = 0.0m;

        [Column("KgMonitoring_IDRawMaterial")]
        public int RawMaterialRawMateria_id { set; get; }

        [ForeignKey("RawMaterialRawMateria_id")]
        public RawMaterial RawMaterial { set; get; } = new RawMaterial();

        public DateTime KgMonitoring_created { set; get; } = DateTime.Now;
        public DateTime KgMonitoring_updated { set; get; } = DateTime.Now;

        public ICollection<Accounting> Accountings { set; get; } = new List<Accounting>();
    }
}
