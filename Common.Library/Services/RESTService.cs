using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;

namespace Statera.Xamarin.Common
{
    public class RESTService
    {

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
                        s.Flush();
                        successAction(JsonConvert.DeserializeObject<T>(content));
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
