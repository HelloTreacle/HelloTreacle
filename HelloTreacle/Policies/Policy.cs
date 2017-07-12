using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloTreacle.Requests.Store;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public class Policy
    {
        private Policy()
        { }

        public Policy(TimeSpan timeSpan, int frequencyThreshold, IEnumerable<string> requestProperties, Action<IOwinContext> onPolicyViolation)
        {
            TimeSpan = timeSpan;
            FrequencyThreshold = frequencyThreshold;
            RequestProperties = requestProperties;
            OnPolicyViolation = onPolicyViolation;
        }

        public TimeSpan TimeSpan { get; }

        public int FrequencyThreshold { get; }

        public IEnumerable<string> RequestProperties { get; }

        public Action<IOwinContext> OnPolicyViolation { get; }

        public async Task<bool> Run(RequestStore requestStore, IOwinContext context)
        {
            var frequency = await requestStore.QueryDuplicateFrequency(TimeSpan, RequestProperties);

            if (frequency >= FrequencyThreshold)
            {
                OnPolicyViolation(context);
                return true;
            }

            return false;
        }

        public IEnumerable<Prerequisite> Prerequisites { get; set; }

        public static Policy WithPrerequisites(params Prerequisite[] prerequisites)
        {
            var requestPolicy = new Policy
            {
                Prerequisites = prerequisites
            };

            return requestPolicy;
        }
    }
}
