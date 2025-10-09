using Microsoft.AspNetCore.DataProtection;

namespace Utility.Security
{
    public class DataProtector
    {
        private readonly IDataProtector _dataProtector;

        public DataProtector(IDataProtectionProvider provider)
        {
            this._dataProtector = provider.CreateProtector("");
        }


    }
}
