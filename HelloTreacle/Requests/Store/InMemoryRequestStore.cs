using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloTreacle.Requests.Properties.Retrieval;
using System.Linq.Dynamic;

namespace HelloTreacle.Requests.Store
{
    public class InMemoryRequestStore : RequestStore
    {
        private readonly List<RequestRepresentation> inMemoryDatabase;

        public InMemoryRequestStore()
            : base(new DefaultRequestPropertyRetriever())
        {
            inMemoryDatabase = new List<RequestRepresentation>();
        }

        public override Task Store(RequestRepresentation request)
        {
            inMemoryDatabase.Add(request);
            return Task.FromResult(0);
        }

        protected override Task<int> QueryDuplicateFrequency(DateTime fromDate, IEnumerable<string> properties)
        {
            var requestsInDate = inMemoryDatabase
                .Where(x => x.TimeStamp >= fromDate)
                .SelectMany(x => x.Properties.Where(p => properties.Contains(p.Key)))
                .Select(x => new { x.Key, x.Value })
                .GroupBy(x => x)
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .ToList();

            return Task.FromResult(!requestsInDate.Any() 
                ? 0 
                : requestsInDate.Select(x => x.Count).Max());
        }
    }
}