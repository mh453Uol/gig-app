using Gig.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gig.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followers { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany<Models.Gig>()
                .WithOne(g => g.Artist)
                .HasForeignKey(g => g.ArtistId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany()
                .HasForeignKey(a => a.GigId);

            builder.Entity<Attendance>()
                .HasOne(a => a.Attendee)
                .WithMany()
                .HasForeignKey(a => a.AttendeeId);

            //Manually configure many to many relationship
            builder.Entity<Following>()
                .HasKey(f => new { f.FollowerId, f.FolloweeId });

            builder.Entity<Following>()
                .HasOne(f => f.Follower)
                .WithMany(f => f.Followees)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<Following>()
                .HasOne(f => f.Followee)
                .WithMany(f => f.Followers)
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            //End 

            //Config Application for application user to following
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithOne(u => u.Followee)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithOne(u => u.Follower)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            //End

            builder.Entity<UserNotification>()
                .HasKey(n => new { n.NotificationId, n.UserId });

            builder.Entity<UserNotification>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<UserNotification>()
                .HasOne(u => u.Notification)
                .WithMany()
                .HasForeignKey(u => u.NotificationId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            builder.Entity<Notification>()
                .HasOne(n => n.Gig)
                .WithMany()
                .HasForeignKey(n => n.GigId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);



            base.OnModelCreating(builder);
        }
    }
}
