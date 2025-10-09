using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class KG_Catalog
    {
        [Key]
        public int KGCatalog_id { set; get; }

        public Guid KGCatalog_guid { set; get; } = Guid.NewGuid();

        public double KGCatalog_cantidad { set; get; } = 0.0d;
    
        public DateTime KGCatalog_created { set; get; } = DateTime.Now;
        public DateTime KGCatalog_updated { set; get; } = DateTime.Now;

        public ICollection<DataCatalogProduct> DataCatalogProducts { set; get; } = new List<DataCatalogProduct>();
    }

}
