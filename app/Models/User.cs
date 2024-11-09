namespace app.Models
{
    public record class User
    {
        public int? Id { get; set ;} = null;
        public string? Login { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
