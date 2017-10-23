namespace Browser.Tests.Requests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The http response tests.
    /// </summary>
    [TestClass]
    public class HttpResponseTests
    {
        /// <summary>
        /// The get title test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TestMethod]
        public async Task GetTitleTest()
        {
            var testList = new List<HttpResponse>()
                               {
                                   new HttpResponse() { Content = "<title>Success</title>" },
                                   new HttpResponse() { Content = "<title id=\"theTitle\">Success</title>" },
                                   new HttpResponse() { Content = "<title class=\"titles\">Success</title>" },
                                   new HttpResponse() { Content = "<title gfdjkgflds>Success</title>" },
                                   new HttpResponse() { Content = "<title className=\"react\">Success</title>" }
                               };

            foreach (var httpResponse in testList)
            {
                Assert.AreEqual(await httpResponse.GetTitle(), "Success");
            }
        }
    }
}