namespace HeroDatingApp.Helpers
{
    public class MessageParameters : PaginationParameters
    {
        public string Username { get; set; }
        public string Container { get; set; } = "Unread";

    }
}