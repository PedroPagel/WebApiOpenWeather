using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Entities;

namespace Repositories.Context
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {
        }

        public DbSet<OpenWeather> OpenWeather { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OpenWeather>().HasKey(r => r.Id);
            builder.Entity<OpenWeather>().Property(r => r.Name).IsRequired();
        }
    }
}
