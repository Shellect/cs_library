namespace app.ViewModels
{
    class RegistrationResponseViewModel
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; } = null;
    }
}