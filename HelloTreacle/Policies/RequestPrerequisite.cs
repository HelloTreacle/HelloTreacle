using System;
using System.Linq;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public abstract class RequestPrerequisite
    {
        public abstract class Path
        {
            public static Prerequisite Equals(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value == path);
            }

            public static Prerequisite NotEquals(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value != path);
            }

            public static Prerequisite Contains(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value.Contains(path));
            }

            public static Prerequisite DoesNotContain(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value.Contains(path) == false);
            }
        }

        public abstract class Header
        {
            public static Prerequisite ValueEquals(string key, string value)
            {
                return new Prerequisite(new Func<IOwinRequest, bool>(
                    owinRequest =>
                    {
                        return owinRequest.Headers.TryGetValue(key, out string[] headerValues)
                            && headerValues.Any(x => x.Equals(value));
                    }));

            }
        }
    }
}