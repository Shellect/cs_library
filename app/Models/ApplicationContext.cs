using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}