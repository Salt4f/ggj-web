using GGJWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GGJWeb.Data
{
    public class DataContext : DbContext
    {
        readonly IConfiguration configuration;

        public DataContext(IConfiguration config)
        {
            configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<HomeModel> Home { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<PostInfo>? PostInfos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.PostInfo);
        }


    }
}
