using Microsoft.EntityFrameworkCore;
using TestMakerFreeApp.Data.Models;

namespace TestMakerFreeApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Quizzes).WithOne(x => x.User);

            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<Quiz>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Quiz>().HasOne(x => x.User).WithMany(x => x.Quizzes);
            modelBuilder.Entity<Quiz>().HasMany(x => x.Questions).WithOne(x => x.Quiz);

            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Question>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Question>().HasOne(x => x.Quiz).WithMany(x => x.Questions);
            modelBuilder.Entity<Question>().HasMany(x => x.Answers).WithOne(x => x.Question);

            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Answer>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Answer>().HasOne(x => x.Question).WithMany(x => x.Answers);

            modelBuilder.Entity<Result>().ToTable("Results");
            modelBuilder.Entity<Result>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Result>().HasOne(x => x.Quiz).WithMany(x => x.Results);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
