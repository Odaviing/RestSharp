﻿using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using OpenQA.Selenium;

namespace APITest
{
    public static class APIHelper
    {
        public static IRestResponse SendJsonApiRequest(string url, string headerName, string headerValue, Dictionary<string, string> body)
        {
            RestClient client = new RestClient(baseUrl: url)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader(name: headerName, value: headerValue);
            foreach (var data in body)
            {
                request.AddParameter(data.Key, data.Value);
            }
            //request.AddJsonBody(body);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            return response;
        }
        public static List<Cookie> ExtractCookie(IRestResponse response)
        {
            List<Cookie> allCookies = new List<Cookie>();
            Cookie result = null;
            foreach (var cookie in response.Cookies)
                allCookies.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));
               // if (cookie.Name.Equals(cookieName))
                    //result = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return allCookies;
        }

        

    }
}
