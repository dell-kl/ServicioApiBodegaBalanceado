using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Price_KG
    {
        [Key]
        public int PriceKG_id { set; get; }

        public Guid PriceKG_guid { set; get; } = Guid.NewGuid();

        public decimal PriceKG_price { set; get; } = 0.0m;

        public DateTime PriceKG_created { set; get; } = DateTime.Now;
        public DateTime PriceKG_updated { set; get; } = DateTime.Now;

        public ICollection<DataCatalogProduct> DataCatalogProducts { set; get; } = new List<DataCatalogProduct>();
    }
}
