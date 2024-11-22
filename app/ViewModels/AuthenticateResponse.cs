namespace app.ViewModels
{
    public class AuthenticateResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; } = [];
        public string? AccessToken { get; set; }
        public string? Login { get; set; }
    }
}