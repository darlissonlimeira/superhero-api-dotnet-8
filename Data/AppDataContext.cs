using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Entities;

namespace SuperHeroApi.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<SuperHero> SuperHeroes { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperHero>().ToTable("superheroes");
        }


    }
}