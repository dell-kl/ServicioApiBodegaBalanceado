using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class DataCatalogProduct
    {
        [Key]
        public int DataCatalogProduct_id { set; get; }

        public Guid DataCatalogProduct_guid { set; get; } = Guid.NewGuid();

        public int DataCatalogProduct_countTotal { set; get; } = 0;

        public DateTime DataCatalogProduct_created { set; get; } = DateTime.Now;
        public DateTime DataCatalogProduct_updated { set; get; } = DateTime.Now;

        [Column("DataCatalogProduct_IDCatalogProduct")]
        public int CatalogProductionCatalogProduction_id { set; get; }

        [Column("DataCatalogProduct_IDKGCatalog")]
        public int KG_CatalogKGCatalog_id { set; get; }

        [Column("DataCatalogProduct_IDPriceKG")]
        public int Price_KGPriceKG_id { set; get; }

        [ForeignKey("CatalogProductionCatalogProduction_id")]
        public CatalogProduction CatalogProduction { set; get; } = new CatalogProduction();

        [ForeignKey("KG_CatalogKGCatalog_id")]
        public KG_Catalog KG_Catalog { set; get; } = new KG_Catalog();

        [ForeignKey("Price_KGPriceKG_id")]
        public Price_KG Price_KG { set; get; } = new Price_KG();


        public ICollection<ProductManufactured> ProductManufactureds { set; get; } = new List<ProductManufactured>();
    
        public ICollection<Sale> Sales { set; get; } = new List<Sale>();    
    }
}
