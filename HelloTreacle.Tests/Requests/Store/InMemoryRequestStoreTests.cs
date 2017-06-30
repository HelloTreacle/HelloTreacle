using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HelloTreacle.Requests;
using HelloTreacle.Requests.Store;
using NUnit.Framework;

namespace HelloTreacle.Tests.Requests.Store
{
    [TestFixture]
    public class InMemoryRequestStoreTests
    {
        protected InMemoryRequestStore InMemoryRequestStore;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InMemoryRequestStore = new InMemoryRequestStore();
        }

        public class QueryFrequency : InMemoryRequestStoreTests
        {
            [Test]
            public async Task Returns_correct_frequency_for_timespan()
            {
                await SaveRequest(new DateTime(2017, 01, 01, 10, 59, 59), new Dictionary<string, object> { { "ip", "1.2.3.4" } });
                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 0), new Dictionary<string, object> { { "ip", "1.2.3.4" } });
                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 1), new Dictionary<string, object> { { "ip", "1.2.3.4" } });
                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 2), new Dictionary<string, object> { { "ip", "1.2.3.4" } });
                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 3), new Dictionary<string, object> { { "ip", "1.2.3.4" } });
                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 4), new Dictionary<string, object> { { "ip", "1.2.3.4" } });

                await SaveRequest(new DateTime(2017, 01, 01, 11, 0, 5), new Dictionary<string, object> { { "ip", "4.5.6.7." } });

                //fake datetime.now
                TimeProvider.SetDateTime(new DateTime(2017, 01, 01, 11, 0, 5));

                var result = await InMemoryRequestStore.QueryDuplicateFrequency(TimeSpan.FromSeconds(5), new[] { "ip" });

                Assert.That(result, Is.EqualTo(5));
            }

            private async Task SaveRequest(DateTime dateTime, Dictionary<string, object> properties)
            {
                var request = new RequestRepresentation
                {
                    TimeStamp = dateTime,
                    Properties = properties.Select(x => new RequestProperty { Key = x.Key, Value = x.Value })
                };

                await InMemoryRequestStore.Store(request);
            }
        }
    }
}
