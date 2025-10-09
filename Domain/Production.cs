using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Production
    {
        [Key]
        public int Production_id { set; get; }
        public Guid Production_guid { set; get; } = Guid.NewGuid();

        public double Production_KGTotal { set; get; } = 0.0d;

        public ESTADO Production_status { set; get; } = ESTADO.PRODUCCION;

        public DateTime Production_created { set; get; } = DateTime.Now;
        public DateTime Production_updated { set; get; } = DateTime.Now;

        [Column("Production_IDCatalogProduction")]
        public int CatalogProductionCatalogProduction_id { set; get; }

        [Column("Production_IDProfile")]
        public int ProfileProfile_id { set; get; }

        [ForeignKey("CatalogProductionCatalogProduction_id")]
        public CatalogProduction CatalogProduction { set; get; } = new CatalogProduction();

        [ForeignKey("ProfileProfile_id")]
        public Profile Profile { set; get; } = new Profile();

        //FK
        public ICollection<MaterialProduction> MaterialProductions { set; get; } = new List<MaterialProduction>();

        public ICollection<ProductManufactured> ProductManufactureds { set; get; } = new List<ProductManufactured>();
    }

    public enum ESTADO
    {
        PRODUCCION = 1,
        ETIQUETAR = 2,
        FABRICADO = 3,
    }
}
