namespace FunApp.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class FunAppDbContext : IdentityDbContext<User>
    {
        public FunAppDbContext(DbContextOptions<FunAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Joke> Jokes { get; set; }

        public DbSet<UserCategory> UserCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Style>()
                .HasOne(st => st.Category)
                .WithMany(s => s.Styles)
                .HasForeignKey(st => st.CategoryId);

            builder
                .Entity<Song>()
                .HasOne(s => s.Style)
                .WithMany(st => st.Songs)
                .HasForeignKey(s => s.StyleId);

            builder
                .Entity<Style>()
                .HasMany(st => st.Songs)
                .WithOne(s => s.Style)
                .HasForeignKey(st => st.StyleId);

            builder
                .Entity<UserCategory>()
                .HasKey(uc => new { uc.CategoryId, uc.UserId });

            builder
                .Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId);

            builder
                .Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.CategoryId);

            builder
                .Entity<UserJokes>()
                .HasKey(uj => new { uj.JokeId, uj.UserId });

            builder
                .Entity<UserJokes>()
                .HasOne(uj => uj.User)
                .WithMany(u => u.Jokes)
                .HasForeignKey(uj => uj.UserId);

            base.OnModelCreating(builder);
        }
    }
}
