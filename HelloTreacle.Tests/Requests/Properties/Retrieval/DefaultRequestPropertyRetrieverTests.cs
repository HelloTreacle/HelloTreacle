using System.Linq;
using HelloTreacle.Requests.Properties.Retrieval;
using Microsoft.Owin;
using NUnit.Framework;

namespace HelloTreacle.Tests.Requests.Properties.Retrieval
{
    [TestFixture]
    public class DefaultRequestPropertyRetrieverTests
    {
        private DefaultRequestPropertyRetriever _defaultRequestPropertyRetriever;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _defaultRequestPropertyRetriever = new DefaultRequestPropertyRetriever();
        }

        [Test]
        public void Retrieves_keys_and_values_of_headers()
        {
            var request = new OwinRequest();
            request.Headers.Add("header1", new[] { "value1" });
            request.Headers.Add("header2", new[] { "value2" });
            request.Headers.Add("header3", new[] { "value1", "value2" });

            var retreivedProperties = _defaultRequestPropertyRetriever.Retrieve(request);

            Assert.That(retreivedProperties.ElementAt(0).Key, Is.EqualTo("header1"));
            Assert.That(retreivedProperties.ElementAt(0).Value, Is.EqualTo("value1"));
            Assert.That(retreivedProperties.ElementAt(1).Key, Is.EqualTo("header2"));
            Assert.That(retreivedProperties.ElementAt(1).Value, Is.EqualTo("value2"));
            Assert.That(retreivedProperties.ElementAt(2).Key, Is.EqualTo("header3"));
            Assert.That(retreivedProperties.ElementAt(2).Value, Is.EqualTo("value1,value2"));
        }
    }
}
