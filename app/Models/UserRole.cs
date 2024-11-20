namespace app.Models
{
    public record class UserRole
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}