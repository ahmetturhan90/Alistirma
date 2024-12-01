using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Business.ApiCall
{
    public interface IApiService
    {
        T PostMethod<T>(object requestObject, string uri, Dictionary<string, string> headers = null, List<RestRequestParamDto> requestParam = null);

        T GetMethod<T>(string uri, Dictionary<string, string> headers = null);
    }
}
