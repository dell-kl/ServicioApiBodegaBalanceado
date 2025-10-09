using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Sale
    {
        [Key]
        public int Sale_id { set; get; }
        public Guid Sale_guid { set; get; } = Guid.NewGuid();

        public int Sale_count { set; get; }

        public decimal Sale_priceTotal { set; get; } = 0.0m;

        public DateTime Sale_created { set; get; } = DateTime.Now;
        public DateTime Sale_updated { set; get; } = DateTime.Now;

        [Column("Sale_IDDataCatalogProduct")]
        public int DataCatalogProductDataCatalogProduct_id { set; get; }

        [ForeignKey("DataCatalogProductDataCatalogProduct_id")]
        public DataCatalogProduct DataCatalogProduct { set; get; } = new DataCatalogProduct();

        public ICollection<Accounting> Accountings { set; get; } = new List<Accounting>();
             
    }
}
