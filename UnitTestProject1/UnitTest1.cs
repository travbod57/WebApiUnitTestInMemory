using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var config = new HttpConfiguration();
            
            //configure web api

            // cannot call 'config.MapHttpAttributeRoutes();' here because it needs the route attribute mappings from the web api project
            InMemoryTesting.WebApiConfig.Register(config);
            
            using (var server = new HttpServer(config))
            {

                var client = new HttpClient(server);

                string url = "http://localhost/api/Values/";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.SendAsync(request))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }

        }
    }
}
