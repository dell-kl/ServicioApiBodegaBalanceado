using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CatalogProduction
    {
        [Key]
        public int CatalogProduction_id { set; get; }
        public Guid CatalogProduction_guid { set; get; } = Guid.NewGuid();
        public string CatalogProduction_name { set; get; } = null!;

        public DateTime CatalogProduction_created { set; get; } = DateTime.Now;
        public DateTime CatalogProduction_updated { set; get; } = DateTime.Now;


        public ICollection<Production> Productions { set; get; } = new List<Production>();
        public ICollection<ImageCatalogProduction> ImageCatalogProductions { set; get; } = new List<ImageCatalogProduction>();

        public ICollection<DataCatalogProduct> DataCatalogProduct { set; get; } = new List<DataCatalogProduct>();
    }
}
