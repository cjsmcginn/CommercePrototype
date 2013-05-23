using CommercePrototype.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercePrototype.Client.Controllers
{
    public class ProxyController : ApiController
    {
        // GET api/proxy
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/proxy/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/proxy
        public HttpResponseMessage Post(ProxyRequest request)
        {
            HttpResponseMessage result = null;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] arr = encoding.GetBytes(request.Content);
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(request.Uri);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.ContentLength = arr.Length;
            httpRequest.KeepAlive = true;
            Stream dataStream = httpRequest.GetRequestStream();
            dataStream.Write(arr, 0, arr.Length);
            dataStream.Close();
            var response = (HttpWebResponse)httpRequest.GetResponse();
            var buffer = new byte[response.ContentLength];
            response.GetResponseStream().Read(buffer, 0, buffer.Length);
            var ms = String.Join("", buffer.Select(x => (char)x).ToArray());
            result = new HttpResponseMessage { Content = new StringContent(ms), StatusCode = response.StatusCode };
            return result;
        }

        // PUT api/proxy/5
        public HttpResponseMessage Put(ProxyRequest request)
        {
            HttpResponseMessage result = null;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] arr = encoding.GetBytes(request.Content);
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(request.Uri);
            httpRequest.Method = "PUT";
            httpRequest.ContentType = "application/json";
            httpRequest.ContentLength = arr.Length;
            httpRequest.KeepAlive = true;
            Stream dataStream = httpRequest.GetRequestStream();
            dataStream.Write(arr, 0, arr.Length);
            dataStream.Close();            
            var response = (HttpWebResponse)httpRequest.GetResponse();
            var buffer = new byte[response.ContentLength];
            response.GetResponseStream().Read(buffer, 0, buffer.Length);
            var ms = String.Join("", buffer.Select(x => (char)x).ToArray());
            result = new HttpResponseMessage{ Content= new StringContent(ms), StatusCode=response.StatusCode};
            return result;
        }

        // DELETE api/proxy/5
        public void Delete(int id)
        {
        }
    }
}
