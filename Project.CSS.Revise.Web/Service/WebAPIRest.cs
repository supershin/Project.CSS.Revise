using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Project.CSS.Revise.Web.Service
{
    public sealed class ApiCallResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public HttpStatusCode? StatusCode { get; set; }
        public string ResponseBody { get; set; } = "";
        public string Endpoint { get; set; } = "";
        public T Data { get; set; }
    }

    public class WebAPIRest
    {
        // ใช้ต่อกับโค้ดเดิมได้ ถ้าอยาก throw เมื่อ fail
        public T RequestPostWebAPI<T>(string urlApi, object jsonData, string authorization = null)
        {
            var r = TryRequestPostWebAPI<T>(urlApi, jsonData, authorization);
            if (!r.Success)
            {
                var code = r.StatusCode.HasValue ? ((int)r.StatusCode.Value + " " + r.StatusCode.Value) : "NO_STATUS";
                var msg = $"POST {r.Endpoint} failed: {r.Message} | Status={code} | Body={(string.IsNullOrWhiteSpace(r.ResponseBody) ? "<empty>" : Truncate(r.ResponseBody, 800))}";
                throw new Exception(msg);
            }
            return r.Data;
        }

        public ApiCallResult<T> TryRequestPostWebAPI<T>(string urlApi, object jsonData, string authorization = null)
        {
            var result = new ApiCallResult<T> { Endpoint = urlApi };

            try
            {
                // เผื่อเครื่อง IIS ต้องบังคับ TLS
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | (SecurityProtocolType)12288; // 12288 = TLS 1.3 บน .NET ที่รองรับ

                var req = (HttpWebRequest)WebRequest.Create(urlApi);
                req.Method = "POST";
                req.Accept = "application/json; charset=utf-8";
                req.ContentType = "application/json; charset=utf-8";
                req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                req.Timeout = 120000;            // 120s พอสำหรับเรียก service; อย่าใช้ 2 ชม. กับ request เดียว
                req.ReadWriteTimeout = 120000;
                req.KeepAlive = true;

                if (!string.IsNullOrEmpty(authorization))
                    req.Headers["Authorization"] = authorization;

                var payload = JsonConvert.SerializeObject(jsonData);
                var buf = Encoding.UTF8.GetBytes(payload);
                req.ContentLength = buf.Length;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(buf, 0, buf.Length);
                }

                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var respStream = resp.GetResponseStream())
                using (var reader = new StreamReader(respStream))
                {
                    var body = reader.ReadToEnd();
                    result.StatusCode = resp.StatusCode;

                    var json = JsonConvert.DeserializeObject<T>(body, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    result.Success = true;
                    result.Data = json;
                    result.ResponseBody = body;

                    return result;
                }
            }
            catch (WebException wx)
            {
                result.Success = false;

                if (wx.Response is HttpWebResponse http)
                {
                    result.StatusCode = http.StatusCode;
                    try
                    {
                        using var rs = http.GetResponseStream();
                        using var rd = new StreamReader(rs);
                        result.ResponseBody = rd.ReadToEnd();
                    }
                    catch { /* ignore */ }
                }

                result.Message = $"WebException: {wx.Status} {(wx.Message ?? "").Trim()}";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Exception: {(ex.Message ?? "").Trim()}";
                return result;
            }
        }

        private static string Truncate(string s, int max)
            => string.IsNullOrEmpty(s) || s.Length <= max ? s : s.Substring(0, max) + "...";
    }
}
