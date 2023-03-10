using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using HeroDatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HeroDatingApp.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) 
                return;

            string[] powers = {"Attractive", "Very Strong", "Can Fly", "Sets Things on Fire", "Laser Eyes", "Doesnt need sleep"};

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                Random random = new Random();
                int randomIndex = random.Next(powers.Length);

                user.UserName = user.UserName.ToLower();
                user.Power = powers[randomIndex];
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);

            }

            await context.SaveChangesAsync();

        }
    }
}