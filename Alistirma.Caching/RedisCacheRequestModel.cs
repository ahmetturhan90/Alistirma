using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Caching
{
    public class RedisCacheRequestModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
