using app.Models;
using app.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public interface IUserService
    {
        public void Create(RegistrationViewModel model);
        public Task<User?> GetUser(LoginViewModel model);
        public User? GetUser(string refreshToken);
    }
    public class UserService(ApplicationContext context) : IUserService
    {
        public async void Create(RegistrationViewModel model)
        {
            context.Users.Add(new User { Login = model.Login, Email = model.Email, Password = model.Password });
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(LoginViewModel model)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
        }

        public User? GetUser(string refreshToken)
        {
            return context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }
    }

    public static class ServiceProviderUsersExtensions
    {
        public static void AddUserService(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
        }
    }
}