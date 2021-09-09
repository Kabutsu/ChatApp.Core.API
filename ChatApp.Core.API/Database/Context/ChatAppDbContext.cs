using Microsoft.EntityFrameworkCore;
using ChatApp.Core.API.Database.Entities;

namespace ChatApp.Core.API.Database.Context
{
    public class ChatAppDbContext : DbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Connection> Connection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(c => c.UserId);
            modelBuilder.Entity<Connection>().HasKey(c => c.ConnectionId);
        }
    }
}
