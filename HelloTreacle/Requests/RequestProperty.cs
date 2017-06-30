namespace HelloTreacle.Requests
{
    public class RequestProperty
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Key + "|" + Value;
        }
    }
}