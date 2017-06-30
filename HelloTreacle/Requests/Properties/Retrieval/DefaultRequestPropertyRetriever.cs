using System.Collections.Generic;
using Microsoft.Owin;

namespace HelloTreacle.Requests.Properties.Retrieval
{
    public class DefaultRequestPropertyRetriever : IRequestPropertyRetriever
    {
        public IEnumerable<RequestProperty> Retrieve(IOwinRequest request)
        {
            var properties = new List<RequestProperty>();

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