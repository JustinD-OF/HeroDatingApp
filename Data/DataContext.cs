using Microsoft.EntityFrameworkCore;
using HeroDatingApp.Entities;

namespace HeroDatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<AppUser> Users {get; set;}
    }
}