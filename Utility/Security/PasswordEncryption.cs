using Microsoft.AspNetCore.Identity;

namespace Utility.Security
{
    public class PasswordEncryption<T> : PasswordHasher<T> where T : class
    {
    
    }
}
