using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Statera.Xamarin.Common
{
    public class RESTService
    {

        public async Task<T> GetAsync<T>(string url)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
            var responseStream = responseObject.GetResponseStream();
            var sr = new StreamReader(responseStream);
            string content = await sr.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private async Task<T> PostAsync<T>(string url, object obj)
        {
            var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            request.Method = "POST";

            byte[] data = ConvertToByteArray(obj);
            request.ContentLength = data.Length;

            using (var requestStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                await requestStream.WriteAsync(data, 0, data.Length);
            }

            WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
            var responseStream = responseObject.GetResponseStream();
            var sr = new StreamReader(responseStream);
            string content = await sr.ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }


        public void Get<T>(string url, Action<T> successAction, Action<WebException> errorAction)
        {
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            try
            {
                httpReq.BeginGetResponse((ar) =>
                {
                    var request = (HttpWebRequest)ar.AsyncState;
                    using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                    {
                        var s = response.GetResponseStream();
                        var sr = new StreamReader(s);
                        var content = sr.ReadToEnd();
                        var obj = JsonConvert.DeserializeObject<T>(content);
                        successAction(obj);
                    }
                }, httpReq);
            }
            catch (WebException ex)
            {
                if (errorAction != null)
                {
                    errorAction(ex);
                }
            }
        }
        public void Post<T>(string url, object obj, Action<T> successAction, Action<WebException> errorAction)
        {
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            try
            {
                var byteArray = ConvertToByteArray(obj);
                httpReq.Method = "POST";
                httpReq.ContentType = "application/json";

                httpReq.BeginGetRequestStream((sr) =>
                {
                    var innerRequest = (HttpWebRequest)sr.AsyncState;
                    var innerStream = innerRequest.EndGetRequestStream(sr);
                    innerStream.Write(byteArray, 0, byteArray.Length);

                    innerRequest.BeginGetResponse((ar) =>
                    {
                        var request = (HttpWebRequest)ar.AsyncState;
                        using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                        {
                            var responseStream = response.GetResponseStream();
                            var responseReader = new StreamReader(responseStream);
                            var content = responseReader.ReadToEnd();
                            responseStream.Flush();
                            successAction(JsonConvert.DeserializeObject<T>(content));
                        }
                    }, innerRequest);
                }, httpReq);
            }
            catch (WebException ex)
            {
                if (errorAction != null)
                {
                    errorAction(ex);
                }
            }
        }

        private byte[] ConvertToByteArray(object obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
