using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloTreacle.Requests.Store;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public class RequestPolicy
    {
        private RequestPolicy()
        { }

        public RequestPolicy(TimeSpan timeSpan, int frequencyThreshold, IEnumerable<string> requestProperties, Action<IOwinContext> onPolicyViolation)
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

        public IEnumerable<Func<IOwinRequest, bool>> Prerequisites { get; set; }

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

        public static RequestPolicy WithPrerequisites(params Func<IOwinRequest, bool>[] prerequisites)
        {
            return WithPrerequisites(PrerequisiteOperator.Or, prerequisites);
        }

        public static RequestPolicy WithPrerequisites(PrerequisiteOperator @operator, params Func<IOwinRequest, bool>[] prerequisiteFunc)
        {
            return new RequestPolicy
            {
                PrerequisitesOperator = @operator,
                Prerequisites = prerequisiteFunc
            };
        }

        public PrerequisiteOperator PrerequisitesOperator { get; set; }
    }
}
