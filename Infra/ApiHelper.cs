using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class ApiHelper
    {
        
        public static string BuildParameters(string BaseURI, string[] parameters, params object[] values)
        {
            if (parameters.Count() > 0 && values.Count() > 0)
            {
                BaseURI += "?";

                for (int index = 0; index < parameters.Length; index++)
                    if (index == 0)
                        BaseURI += $"{parameters[index]}={values[index]}";
                    else
                        BaseURI += $"&{ parameters[index]}={ values[index]}";
            }
            return BaseURI;
        }

        public static HttpResponseMessage SendGetRequest(string uri)
        {
            using (var client = new HttpClient())
            {
                return client.GetAsync(uri).Result;
            }
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
