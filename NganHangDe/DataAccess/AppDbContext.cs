
using Microsoft.EntityFrameworkCore;
using NganHangDe.Models;
using System.Reflection.Metadata;

namespace NganHangDe.DataAccess;
public class AppDbContext : DbContext
{
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Answer> Answers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databaseFilePath = App.GetDatabaseFilePath();
        optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;AttachDbFilename={databaseFilePath};Database=NganHangDe;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuizQuestion>()
            .HasKey(qq => new { qq.QuizId, qq.QuestionId });

        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Quiz)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuizId);

        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Question)
            .WithMany(q => q.QuizQuestions)
            .HasForeignKey(qq => qq.QuestionId);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .IsRequired(false);
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Category)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CategoryId);
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
        .HasForeignKey(a => a.QuestionId);
        modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name="TEst 1", Info="Test" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "TEst 2", Info = "Test" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "TEst 3", Info = "Test", ParentCategoryId = 1 });
        for (int i = 1; i <= 10; i++)
        {
            modelBuilder.Entity<Question>().HasData(new Question { Id = i, Name = $"Question {i}", Text=$"Question Text {i}", CategoryId = i/5 +1 });
            for(int j = 0; j < 4; j++)
            {
                modelBuilder.Entity<Answer>().HasData(new Answer { Id = i * 4 + j, Grade= j==0? 1.0: 0, Text=$"Answer Text {i} - {j}", QuestionId=i });
            }
        }
    }

}
