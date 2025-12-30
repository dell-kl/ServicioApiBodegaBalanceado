using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ProductManufactured
    {
        [Key]
        public int ProductManufactured_id { set; get; }

        public Guid ProductManufactured_guid { set; get; } = Guid.NewGuid();

        public int ProductManufactured_count { set; get; } 

        public DateTime ProductManufactured_created { set; get; } = DateTime.Now;
        public DateTime ProductManufactured_updated { set; get; } = DateTime.Now;

        [Column("ProductManufactured_IDDataCatalogProduct")]
        public int? DataCatalogProductDataCatalogProduct_id { set; get; } = null;

        [Column("ProductManufactured_IDProduction")]
        public int ProductionProduction_id { set; get; }

        [ForeignKey("DataCatalogProductDataCatalogProduct_id")]
        public DataCatalogProduct? DataCatalogProduct { set; get; } = null;

        [ForeignKey("ProductionProduction_id")]
        public Production Production { set; get; } = null!;
    }
}
