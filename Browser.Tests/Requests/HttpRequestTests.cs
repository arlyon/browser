namespace Browser.Tests.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Threading.Tasks;

    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The http request tests.
    /// </summary>
    [TestClass]
    public class HttpRequestTests
    {
        /// <summary>
        /// The get async test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TestMethod]
        public async Task GetAsyncTest()
        {
            var requestList = new List<Url>()
                                  {
                                      Url.FromString("www.reddit.com"),
                                      Url.FromString("arlyon.co"),
                                      Url.FromString("facebook.com"),
                                      Url.FromString("httpstat.us/200"),
                                  };

            foreach (var url in requestList)
            {
                try
                {
                    var page = await HttpRequest.GetAsync(url);
                    Assert.AreEqual(page.Status, HttpStatusCode.OK);
                }
                catch (WebException e)
                {
                    Assert.Inconclusive(e.Message);
                }

            }

            var errorList = new List<Url>()
                {
                    Url.FromString("httpstat.us/400"),
                    Url.FromString("httpstat.us/401"),
                    Url.FromString("httpstat.us/403"),
                    Url.FromString("httpstat.us/404"),
                    Url.FromString("httpstat.us/500"),
                    Url.FromString("httpstat.us/502"),
                    Url.FromString("httpstat.us/503"),
                };

            foreach (var url in errorList)
            {
                try
                {
                    await Assert.ThrowsExceptionAsync<WebException>(async () => await HttpRequest.GetAsync(url));
                }
                catch (WebException e)
                {
                    Assert.Inconclusive(e.Message);
                }
            }
        }

        /// <summary>
        /// The get favicon test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TestMethod]
        public async Task GetFaviconAsyncTest()
        {
            Image g = await HttpRequest.GetFaviconAsync(Url.FromString("www.reddit.com"));
            Assert.IsNotNull(g);
        }
    }
}