namespace kiona.io.Configuration
{
    public class Bot
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }

    public class Moderation
    {
        public bool filter { get; set; }
    }

    public class API
    {
        public Bot Bot { get; set; }
        public Moderation Moderation { get; set; }
    }
}
