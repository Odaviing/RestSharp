using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using OpenQA.Selenium;

namespace APITest
{
    public static class APIHelper
    {
        public static IRestResponse CreatePOST(string url, string headerName, string headerValue, Dictionary<string, string> body)
        {
            RestClient client = new RestClient(baseUrl: url)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader(name: headerName, value: headerValue);
            request.AddJsonBody(body);
            request.RequestFormat = DataFormat.Json;
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

        

    }
}
