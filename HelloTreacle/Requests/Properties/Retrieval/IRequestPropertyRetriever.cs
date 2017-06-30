using System.Collections.Generic;
using Microsoft.Owin;

namespace HelloTreacle.Requests.Properties.Retrieval
{
    public interface IRequestPropertyRetriever
    {
        IEnumerable<RequestProperty> Retrieve(IOwinRequest request);
    }
}