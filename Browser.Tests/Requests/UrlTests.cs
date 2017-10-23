namespace Browser.Tests.Requests
{
    using System;
    using System.Collections.Generic;

    using Browser.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The url tests.
    /// </summary>
    [TestClass]
    public class UrlTests
    {
        /// <summary>
        /// The equals test.
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            var urlDict = new Dictionary<Url, Url>
                              {
                                  { Url.FromString("www.facebook.com"), Url.FromString("www.facebook.com") },
                                  { Url.FromString("www.reddit.com"), Url.FromString("www.reddit.com") },
                                  { Url.FromString("reddit.com"), Url.FromString("reddit.com") },
                              };

            foreach (var pair in urlDict)
            {
                Assert.AreEqual(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// The get hash code test.
        /// </summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            var urlDict = new Dictionary<Url, int>
                {
                    { Url.FromString("www.facebook.com"), -785007201 },
                    { Url.FromString("www.reddit.com"), 1682704544 },
                    { Url.FromString("reddit.com"), 932484283 },
                };

            foreach (var pair in urlDict)
            {
                Assert.AreEqual(pair.Key.HashCode, pair.Value);
            }
        }

        /// <summary>
        /// The to string test.
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var urlDict = new Dictionary<Url, string>
                              {
                                  { Url.FromString("www.facebook.com"), "http://www.facebook.com/" },
                                  { Url.FromString("www.reddit.com"), "http://www.reddit.com/" },
                                  { Url.FromString("reddit.com"), "http://reddit.com/" },
                              };

            foreach (var pair in urlDict)
            {
                Assert.AreEqual(pair.Key.ToString(), pair.Value);
            }
        }

        /// <summary>
        /// The url test 1.
        /// </summary>
        [TestMethod]
        public void UrlFromString()
        {
            var urlDict = new Dictionary<Url, Url>
                              {
                                  { Url.FromString("www.facebook.com"), new Url("http://", "www.facebook.com", "/") },
                                  { Url.FromString("www.reddit.com"), new Url("http://", "www.reddit.com", "/") },
                                  { Url.FromString("reddit.com"), new Url("http://", "reddit.com", "/") },
                              };

            foreach (var pair in urlDict)
            {
                Assert.AreEqual(pair.Key, pair.Value);
            }

            var failDict = new List<string>
                               {
                                   "harp://fds.fds/",
                                   "jump://"
                               };

            foreach (var fail in failDict)
            {
                Assert.ThrowsException<InvalidUrlException>(() => Url.FromString(fail));
            }
        }
    }
}