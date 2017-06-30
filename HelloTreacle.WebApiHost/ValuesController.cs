using System.Collections.Generic;
using System.Web.Http;

namespace HelloTreacle.WebApiHost
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }
    }
}
