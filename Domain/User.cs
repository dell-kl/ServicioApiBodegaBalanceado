using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public int User_id { set; get; }

        public Guid User_guid { set; get; } = Guid.NewGuid();

        public string User_firstName { set; get; } = null!;

        public string User_lastName { set; get; } = null!;

        public string User_username { set; get; } = null!;

        public string User_CI { set; get; } = null!;
        public string User_cel { set; get; } = null!;
    
        public string User_email { set; get; } = null!;

        public string User_password { set; get; } = null!;

        public DateTime User_created { set; get; } = DateTime.Now;
        public DateTime User_updated { set; get; } = DateTime.Now;

        public ICollection<Profile> Profiles { set; get; } = new List<Profile>();
    }
}
