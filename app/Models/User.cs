namespace app.Models
{
    public record class User
    {
        public int Id { get; set ;}
        public required string Login { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
