namespace app.ViewModels {
    public class RegistrationResponseModel
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; } = null;
    }
}