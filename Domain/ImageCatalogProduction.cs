using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ImageCatalogProduction
    {
        [Key]
        public int ImageCatalogProduction_id { set; get; }

        public Guid ImageCatalogProduction_guid { set; get; } = Guid.NewGuid();

        public string ImageCatalogProduction_name { set; get; } = null!;

        public DateTime ImageCatalogProduction_created { set; get;} = DateTime.Now;
        public DateTime ImageCatalogProduction_updted { set; get; } = DateTime.Now;

        [Column("ImageCatalogProduction_IDCatalogProduction")]
        public int CatalogProductionCatalogProduction_id { set; get; }

        [ForeignKey("CatalogProductionCatalogProduction_id")]
        public CatalogProduction CatalogProduction { set; get; } = new CatalogProduction();
    }
}
