using Microsoft.Owin;
using System.Collections.Generic;
using System.Linq;

namespace HelloTreacle.Requests.Properties.Retrieval
{
    public class DefaultRequestPropertyRetriever : IRequestPropertyRetriever
    {
        public IEnumerable<RequestProperty> Retrieve(IOwinRequest request)
        {
            var properties = new List<RequestProperty>();

            var headerRequestProperties = request.Headers.Select(header => new RequestProperty
            {
                Key = header.Key,
                Value = string.Join(",", header.Value.Select(v => v))
            }).ToList();

            properties.AddRange(headerRequestProperties);

            //todo: add many more
            properties.Add(new RequestProperty
            {
                Key = "LocalIpAddress",
                Value = request.LocalIpAddress
            });

            return properties;
        }
    }
}