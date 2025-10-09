using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class RawMaterial
    {
        [Key]   
        public int RawMateria_id { set; get; }

        public Guid RawMaterial_guid { set; get; } = Guid.NewGuid();

        public string RawMaterial_name { set; get; } = null!;

        public double RawMaterial_KgTotal { set; get; } = 0.0d;

        public DateTime RawMaterial_created { set; get; } = DateTime.Now;
        public DateTime RawMaterial_updated { set; get; } = DateTime.Now;

        public ICollection<ImageRawMaterial> ImageRawMaterials { set; get; } = new List<ImageRawMaterial>();
        public ICollection<KgMonitoring> KgMonitorings { set; get; } = new List<KgMonitoring>();
        public ICollection<MaterialProduction> MaterialProductions { set; get; } = new List<MaterialProduction>();
    }
}
