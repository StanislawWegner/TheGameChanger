using Microsoft.EntityFrameworkCore;

namespace TheGameChanger.Entities
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<TypeOfGame> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(eb =>
            {
                eb.Property(p => p.Name).IsRequired().HasMaxLength(25);
                eb.Property(p => p.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(p => p.UpdatedDate).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<TypeOfGame>(eb =>
            {
                eb.Property(p => p.Name).IsRequired().HasMaxLength(25);
                eb.Property(p => p.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(p => p.UpdatedDate).ValueGeneratedOnUpdate();
            });
        }

       

        
    }


}