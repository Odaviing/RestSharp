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
                {"Username", "Odaviing"},
                {"Password", "theelderscrolls5"}
            };
            IRestResponse response = APIHelper.CreatePOST("https://www.moxfield.com/account/signin", "content-type", "application/json", body);
            var cookie = APIHelper.ExtractCookie(response, "TiPMix");

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.moxfield.com");
           
            driver.Manage().Cookies.AddCookie(cookie);

            
            System.Threading.Thread.Sleep(10000);
            //Assert.Equal("200", response.StatusCode.ToString());
        }
    }
}
