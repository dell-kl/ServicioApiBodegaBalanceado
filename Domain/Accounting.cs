using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Accounting
    {
        [Key]
        public int Accounting_id { set; get; }

        public Guid Accounting_guid { set; get; } = Guid.NewGuid();

        public decimal Accounting_ingreso { set; get; } = 0.0m;

        public decimal Accounting_egreso { set; get; } = 0.0m;

        public decimal Accounting_saldo { set; get; } = 0.0m;

        [Column("Accounting_IDKgMonitoring")]
        public int? KgMonitoringKgMonitoring_id { set; get; }

        [ForeignKey("KgMonitoringKgMonitoring_id")]
        public KgMonitoring? KgMonitoring { set; get; }

        [Column("Accounting_IdSales")]
        public int? SaleSale_id { set; get; }

        [ForeignKey("SaleSale_id")]
        public Sale? Sale { set; get; } 

        public DateTime Accounting_created { set; get; } = DateTime.Now;
        public DateTime Accounting_updated { set; get; } = DateTime.Now;
    }
}
