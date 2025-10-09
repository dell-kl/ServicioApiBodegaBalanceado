using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Profile
    {
        [Key]
        public int Profile_id { set; get; }
    
        public Guid Profile_guid { set; get; } = Guid.NewGuid();

        [Column("Profile_IDUser")]
        public int UserUser_id { set; get; }

        [Column("Profile_IDRol")]
        public int RolRol_id { set; get; }


        [ForeignKey("UserUser_id")]
        public User? User { set; get; }


        [ForeignKey("RolRol_id")]
        public Rol? Rol { set; get; }

        public DateTime Profile_created { set; get; } = DateTime.Now;
        public DateTime Profile_updated { set; get; } = DateTime.Now;
    }
}
