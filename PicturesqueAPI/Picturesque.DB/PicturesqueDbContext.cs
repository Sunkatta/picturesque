using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Picturesque.Domain;

namespace Picturesque.DB
{
    public sealed class PicturesqueDbContext : IdentityDbContext<User>
    {
        public PicturesqueDbContext(DbContextOptions<PicturesqueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<GameScore> GameScores { get; set; }

        public DbSet<UserStatistics> UserStatistics { get; set; }

        public DbSet<PicturesCategories> PicturesCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(PicturesqueDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
