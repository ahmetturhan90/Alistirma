using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Caching
{
    public interface ICacheManager
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task Clear(string key);
        void ClearAll();
    }
}
