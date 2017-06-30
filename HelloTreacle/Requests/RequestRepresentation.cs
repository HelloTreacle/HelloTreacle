using System;
using System.Collections.Generic;

namespace HelloTreacle.Requests
{
    public class RequestRepresentation
    {
        public RequestRepresentation()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public DateTime TimeStamp { get; set; }

        public IEnumerable<RequestProperty> Properties { get; set; }
    }
}
