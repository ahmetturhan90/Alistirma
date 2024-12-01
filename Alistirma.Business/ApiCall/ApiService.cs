using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Business.ApiCall
{
    public static class TokenVariables
    {
        public static string TokenData { get; set; }
        public class Token
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
        }
        public class ApiService
        {
            public ApiService()
            {
            }

            public static async Task<T> PostMethod<T>(object requestObject, string uri, Dictionary<string, string> headers = null, List<RestRequestParamDto> requestParam = null)
            {
                uri = "https://77.245.158.107:44367/api/app/" + uri;
                headers = new Dictionary<string, string>();
                var options = new RestClientOptions(uri)
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                if (String.IsNullOrEmpty(TokenVariables.TokenData))
                {
                    var token = await GetToken();
                }
                var client = new RestClient(options);
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };
                headers.Add("Authorization", "Bearer " + TokenVariables.TokenData);
                headers.Add("Content-Type", "application/json");
                var request = new RestRequest() { Method = Method.Post };

                if (requestParam != null && requestParam?.Count > 0)
                {
                    for (int i = 0; i < requestParam.Count; i++)
                    {
                        //request.AddParameter(new Parameter(requestParam[i].name, requestParam[i].value));
                    }
                }

                var result = await GetResultAsync<T>(client, request, requestObject, headers);
                if (result == null)
                {
                    GetToken();
                }
                return result;
            }
            private static async Task<Token> GetToken()
            {
                try
                {
                    var options = new RestClientOptions("https://77.245.158.107:44370/connect/token")
                    {
                        RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                    };
                    var client = new RestClient(options);
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };
                    var request = new RestRequest();
                    request.Timeout = TimeSpan.FromSeconds(60);
                    request.Method = Method.Post;
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("client_id", "CyberService_Swagger");
                    request.AddParameter("grant_type", "password");
                    request.AddParameter("username", "username");
                    request.AddParameter("password", "password");
                    request.AddParameter("client_secret", "");
                    request.AddParameter("scope", "CyberService phone email profile roles openid");
                    var result = await GetResultAsync<Token>(client, request);
                    TokenVariables.TokenData = result?.access_token;
                    return result;

                }
                catch (Exception ex)
                {
                    return null;

                }
            }
            public async Task<T> GetMethod<T>(string uri, Dictionary<string, string> headers = null)
            {
                headers = new Dictionary<string, string>();
                headers.Add("content-type", "application/json");
                headers.Add("cache-control", "no-cache");
                var client = new RestClient(uri);
                var request = new RestRequest() { Method = Method.Get, RequestFormat = DataFormat.Json, AlwaysMultipartFormData = true };
                var result = await GetResultAsync<T>(client, request, null, headers);

                return result;
            }



            private static async System.Threading.Tasks.Task<T> GetResultAsync<T>(RestClient client, RestRequest request, object requestObject = null, Dictionary<string, string> headers = null)
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                if (requestObject != null)
                {
                    string jsonString = JsonConvert.SerializeObject(requestObject);
                    request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                }

                var response = client.ExecuteAsync(request);
                //response.Wait();
                var restResponse = await response;

                if (restResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Hata");

                return JsonConvert.DeserializeObject<T>(restResponse.Content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }


        }
    }
}