using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloTreacle.Requests.Properties.Retrieval;
using Microsoft.Owin;

namespace HelloTreacle.Requests.Store
{
    public abstract class RequestStore
    {
        private readonly IRequestPropertyRetriever requestPropertyRetriever;

        protected RequestStore(IRequestPropertyRetriever requestPropertyRetriever)
        {
            this.requestPropertyRetriever = requestPropertyRetriever;
        }

        public IEnumerable<IEnumerable<string>> PropertyKeys { get; set; }

        public Task Store(IOwinRequest request)
        {
            var requestRepresentation = new RequestRepresentation();

            var properties = requestPropertyRetriever.Retrieve(request);
            requestRepresentation.Properties = properties;

            return Store(requestRepresentation);
        }

        public abstract Task Store(RequestRepresentation request);

        public Task<int> QueryDuplicateFrequency(TimeSpan timeSpan, IEnumerable<string> properties)
        {
            var fromDate = TimeProvider.Now() - timeSpan;
            return QueryDuplicateFrequency(fromDate, properties);
        }

        protected abstract Task<int> QueryDuplicateFrequency(DateTime fromDate, IEnumerable<string> properties);
    }
}