using app.Models;
using app.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Создаёт и возвращает нового пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User</returns>
        public Task<User> Create(RegistrationViewModel model);
        public Task<User?> GetUser(LoginViewModel model);
        public User? GetUser(string refreshToken);
    }
    public class UserService(ApplicationContext context) : IUserService
    {
        public async Task<User> Create(RegistrationViewModel model)
        {
            User user = new()
            { Login = model.Login, Email = model.Email, Password = model.Password };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
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
            services.AddScoped<IUserService, UserService>();
        }
    }
}