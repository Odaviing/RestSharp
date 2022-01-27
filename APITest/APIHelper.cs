using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using OpenQA.Selenium;
using System.IO;

namespace APITest
{
    public static class APIHelper
    {
        public static IRestResponse SendJsonApiRequest(object body, Dictionary<string, string> headers, string url, Method type)
        {
            RestClient client = new RestClient(baseUrl: url)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            bool isBodyJson = false;
            foreach (var header in headers)
            {
                if (header.Value.Contains("application/json"))
                {
                    isBodyJson = true;
                    break;
                }
            }

            if (!isBodyJson)
            {
                foreach (var data in (Dictionary<string, string>)body)
                {
                    request.AddParameter(data.Key, data.Value);
                }
            }
            else
            {
                request.AddJsonBody(body);
                request.RequestFormat = DataFormat.Json;
            }

            IRestResponse response = client.Execute(request);
            return response;
        }
        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie result = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    result = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return result;
        }

        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> res = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                res.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));

            return res;
        }

        public static byte[] SendJsonApiRequest(string imageUrl)
        {
            var client = new RestClient("https://www.nutscomputergraphics.com/wp-content/uploads/2020/02/Seb_cover.jpg");
            var request2 = new RestRequest(Method.GET);
            byte[] imageAsBytes = client.DownloadData(request2);
            return imageAsBytes;
        }
    }


        public class Code
    {
        public object RespCode { get; set; }
    }
    
}
