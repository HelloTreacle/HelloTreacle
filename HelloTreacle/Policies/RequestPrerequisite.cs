namespace HelloTreacle.Policies
{
    public abstract class RequestPrerequisite
    {
        public class Path
        {
            public static Prerequisite Equals(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value == path, true);
            }

            public static Prerequisite NotEquals(string path)
            {
                return new Prerequisite(owinRequest => owinRequest.Path.Value != path, true);
            }
        }
    }
}