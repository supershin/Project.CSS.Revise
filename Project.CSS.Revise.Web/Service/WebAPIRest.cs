using Newtonsoft.Json;
using System.Net;

namespace Project.CSS.Revise.Web.Service
{
    public class WebAPIRest
    {
        public T RequestPostWebAPI<T>(string UrlAPI, object JsonData, string Authorization = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(UrlAPI);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            if (!string.IsNullOrEmpty(Authorization))
                httpWebRequest.Headers.Set("Authorization", Authorization);

            httpWebRequest.Timeout = 7200000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(JsonData); // ✅ Replaced old serializer
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var jsonResult = JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                // Optional: check status properties
                var propStatus = jsonResult?.GetType().GetProperty("status");
                var propSuccess = jsonResult?.GetType().GetProperty("Success");

                if (propStatus != null && Convert.ToInt32(propStatus.GetValue(jsonResult)) != 1)
                    throw new Exception(jsonResult.GetType().GetProperty("message")?.GetValue(jsonResult)?.ToString());

                if (propSuccess != null && !(bool)propSuccess.GetValue(jsonResult))
                {
                    var msg = jsonResult.GetType().GetProperty("Message")?.GetValue(jsonResult)?.ToString()
                           ?? jsonResult.GetType().GetProperty("message")?.GetValue(jsonResult)?.ToString();
                    throw new Exception(msg);
                }

                return jsonResult;
            }
        }
    }
}
