using Microsoft.EntityFrameworkCore;
using HeroDatingApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HeroDatingApp.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Photo> Photos {get; set;}
        public DbSet<UserLike> Likes {get; set;}
        public DbSet<Message> Messages {get; set;}
        public DbSet<Group> Groups {get; set;}
        public DbSet<Connection> Connections {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(userrole => userrole.UserRoles)
                .WithOne(user => user.User)
                .HasForeignKey(userrole => userrole.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(userrole => userrole.UserRoles)
                .WithOne(user => user.Role)
                .HasForeignKey(userrole => userrole.RoleId)
                .IsRequired();


            builder.Entity<UserLike>()
                .HasKey(k => new {k.SourceUserId, k.TargetUserId});

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.TargetUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);
                // When we will use SQL server, We cannot have Cascade twice, so we need one time 'NoAction'

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}