using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rol
    {
        [Key]
        public int Rol_id { set; get; }

        public Guid Rol_guid { set; get; } = Guid.NewGuid();

        public string Rol_name { set; get; } = null!;

        public bool Rol_status { set; get; } = true;

        public DateTime Rol_created { set; get; } = DateTime.Now;
        public DateTime Rol_updated { set; get; } = DateTime.Now;

        public ICollection<Profile> Profiles { set; get; } = new List<Profile>();
    }
}
