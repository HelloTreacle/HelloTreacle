using System;
using Microsoft.Owin;

namespace HelloTreacle.Policies
{
    public class RequestPath
    {
        public static Func<IOwinRequest, bool> Equals(string path)
        {
            Func<IOwinRequest, bool> checker = i => i.Path.HasValue && i.Path.Value == path;
            return checker;
        }

        public static Func<IOwinRequest, bool> NotEquals(string path)
        {
            Func<IOwinRequest, bool> checker = i => i.Path.HasValue && i.Path.Value != path;
            return checker;
        }
    }
}