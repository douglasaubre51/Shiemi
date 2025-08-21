using Microsoft.EntityFrameworkCore;
using Shiemi.ChatApi.Models;

namespace Shiemi.ChatApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Main> Mains { get; set; }
        public DbSet<WaitingRoom> WaitingRooms { get; set; }
        public DbSet<PrivateRoom> PrivateRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
