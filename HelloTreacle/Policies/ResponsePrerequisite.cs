namespace HelloTreacle.Policies
{
    public abstract class ResponsePrerequisite
    {
        public class StatusCode
        {
            public static Prerequisite Equals(int statusCode)
            {
                return new Prerequisite(owinResponse => owinResponse.StatusCode == statusCode, true);
            }
        }
    }
}