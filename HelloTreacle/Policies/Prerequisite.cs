using System;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public class Prerequisite
    {
        public Prerequisite(Func<IOwinRequest, bool> requestCondition, bool positiveOutcome)
        {
            RequestCondition = requestCondition;
            PositiveOutcome = positiveOutcome;
        }

        public Prerequisite(Func<IOwinResponse, bool> responseCondition, bool positiveOutcome)
        {
            ResponseCondition = responseCondition;
            PositiveOutcome = positiveOutcome;
        }

        public Func<IOwinRequest, bool> RequestCondition { get; }

        public bool PositiveOutcome { get; }

        public Func<IOwinResponse, bool> ResponseCondition { get; }
    }
}