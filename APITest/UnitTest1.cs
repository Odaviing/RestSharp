using System;
using System.Collections.Generic;
using Xunit;
using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace APITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                {"tes5alduin@gmail.com", "theelderscrolls5"}
            };
            IRestResponse response = APIHelper.CreatePOST("https://www.mtggoldfish.com/", "content-type", "application/json", body);
            var cookie = APIHelper.ExtractCookie(response, "_pbjs_userid_consent_data");

            IWebDriver driver = new ChromeDriver();            
            driver.Manage().Cookies.AddCookie(cookie);

            driver.Navigate().GoToUrl("https://www.mtggoldfish.com/");
            System.Threading.Thread.Sleep(10000);
            //Assert.Equal("200", response.StatusCode.ToString());
        }
    }
}
