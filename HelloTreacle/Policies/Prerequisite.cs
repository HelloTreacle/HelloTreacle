using System;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public class Prerequisite
    {
        public Prerequisite(Func<IOwinRequest, bool> requestCondition)
        {
            RequestCondition = requestCondition;
        }

        public Prerequisite(Func<IOwinResponse, bool> responseCondition)
        {
            ResponseCondition = responseCondition;
        }

        public Func<IOwinRequest, bool> RequestCondition { get; }

        public Func<IOwinResponse, bool> ResponseCondition { get; }
    }
}