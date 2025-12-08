using Domain;
using Utility.Security;

namespace Data.Seeders
{
    public class Seeder
    {

        private readonly ApplicationDbContext _context;
        public Seeder(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async void cargarDatos()
        {
            if (!_context.Rol.Any())
            {
                await _context.Rol.AddAsync(new Domain.Rol()
                {
                    Rol_name = "VENDEDOR",
                    Rol_status = true
                });

                await _context.Rol.AddAsync(new Domain.Rol()
                {
                    Rol_name = "ADMINISTRADOR",
                    Rol_status = true
                });

                _context.SaveChanges();
            }


            if (!_context.User.Any())
            {

                PasswordEncryption<User> encriptar = new PasswordEncryption<User>();

                await _context.User.AddAsync(new Domain.User()
                {
                    User_firstName = "Veronica",
                    User_lastName = "Ponce",
                    User_username = "verito",
                    User_CI = "1102935341",
                    User_cel = "0987168381",
                    User_email = "dennis-ponce07@outlook.com",
                    User_password = encriptar.HashPassword(null, "veronica123")
                });


                await _context.User.AddAsync(new Domain.User()
                {
                    User_firstName = "Dennis",
                    User_lastName = "Ponce",
                    User_username = "dell-kl",
                    User_CI = "1754090106",
                    User_cel = "0987168381",
                    User_email = "poncedennys2005071@gmail.com",
                    User_password = encriptar.HashPassword(null, "dennis123")
                });

                _context.SaveChanges();
            }

            if (!_context.Profile.Any())
            {
                await _context.Profile.AddAsync(new Domain.Profile()
                {
                    UserUser_id = 1,
                    RolRol_id = 1
                });
                await _context.Profile.AddAsync(new Domain.Profile()
                {
                    UserUser_id = 2,
                    RolRol_id = 2
                });

                _context.SaveChanges();
            }


        }
    }
}
