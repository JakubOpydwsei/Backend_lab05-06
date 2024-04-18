using Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class QuizDbContext : DbContext
    {
        public DbSet<QuizEntity> Quizzes { get; set; }
        public DbSet<QuizItemEntity> QuizItems { get; set; }
        public DbSet<QuizItemUserAnswerEntity> UserAnswers { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("" +
                "Data Source=DESKTOP-QD5U2FA\\SQLEXPRESS;Initial Catalog=MyQuizDatabase;Integrated Security=True;Encrypt=false;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuizItemUserAnswerEntity>()
                .HasOne<QuizEntity>()
                .WithMany()
                .HasForeignKey(a => a.QuizId);

            modelBuilder.Entity<QuizItemUserAnswerEntity>()
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(a => a.UserId);



            modelBuilder.Entity<UserEntity>().HasData(new UserEntity { Id = 1, Email = "email@email.com", Password = "password123!@#" });



            modelBuilder.Entity<QuizItemUserAnswerEntity>()
                .HasOne(e => e.QuizItem);

            modelBuilder.Entity<QuizItemAnswerEntity>()
                .HasData(
                    new QuizItemAnswerEntity() { Id = 1, Answer = "1" },
                    new QuizItemAnswerEntity() { Id = 2, Answer = "2" },
                    new QuizItemAnswerEntity() { Id = 3, Answer = "3" },
                    new QuizItemAnswerEntity() { Id = 4, Answer = "4" },
                    new QuizItemAnswerEntity() { Id = 5, Answer = "5" },
                    new QuizItemAnswerEntity() { Id = 6, Answer = "6" },
                    new QuizItemAnswerEntity() { Id = 7, Answer = "7" },
                    new QuizItemAnswerEntity() { Id = 8, Answer = "8" },
                    new QuizItemAnswerEntity() { Id = 9, Answer = "9" },
                    new QuizItemAnswerEntity() { Id = 10, Answer = "0" }
                );
            /////////////////////////////////////////////////////////

            modelBuilder.Entity<QuizItemEntity>()
                .HasData(
                    new QuizItemEntity()
                    {
                        Id = 1,
                        Question = "2 + 3",
                        CorrectAnswer = "5"
                    },
                    new QuizItemEntity()
                    {
                        Id = 2,
                        Question = "2 * 3",
                        CorrectAnswer = "6"
                    },
                    new QuizItemEntity()
                    {
                        Id = 3,
                        Question = "8 - 3",
                        CorrectAnswer = "5"
                    },
                    new QuizItemEntity()
                    {
                        Id = 4,
                        Question = "8 : 2",
                        CorrectAnswer = "4"
                    },
                    new QuizItemEntity()
                    {
                        Id = 5,
                        Question = "1 * 1",
                        CorrectAnswer = "1"
                    },
                    new QuizItemEntity()
                    {
                        Id = 6,
                        Question = "6 * 6",
                        CorrectAnswer = "36"
                    }
                );

            modelBuilder.Entity<QuizEntity>()
                .HasData(
                    new QuizEntity()
                    {
                        Title = "Matematyka",
                        Id = 1
                    },
                    new QuizEntity()
                    {
                        Title = "Arytmetyka",
                        Id = 2
                    }
                );

            modelBuilder.Entity<QuizEntity>()
                .HasMany<QuizItemEntity>(q => q.Items)
                .WithMany(e => e.Quizzes)
                .UsingEntity(
                    join => join.HasData(
                        new { QuizzesId = 1, ItemsId = 1 },
                        new { QuizzesId = 1, ItemsId = 2 },
                        new { QuizzesId = 1, ItemsId = 3 },
                        new { QuizzesId = 2, ItemsId = 4 },
                        new { QuizzesId = 2, ItemsId = 5 },
                        new { QuizzesId = 2, ItemsId = 6 }
                    )
                );

            modelBuilder.Entity<QuizItemEntity>()
                .HasMany<QuizItemAnswerEntity>(q => q.IncorrectAnswers)
                .WithMany(e => e.QuizItems)
                .UsingEntity(join => join.HasData(
                        // "2 + 3"
                        new { QuizItemsId = 1, IncorrectAnswersId = 1 },
                        new { QuizItemsId = 1, IncorrectAnswersId = 2 },
                        new { QuizItemsId = 1, IncorrectAnswersId = 3 },
                        // "2 * 3"
                        new { QuizItemsId = 2, IncorrectAnswersId = 3 },
                        new { QuizItemsId = 2, IncorrectAnswersId = 4 },
                        new { QuizItemsId = 2, IncorrectAnswersId = 7 },
                        // "8 - 3"
                        new { QuizItemsId = 3, IncorrectAnswersId = 1 },
                        new { QuizItemsId = 3, IncorrectAnswersId = 3 },
                        new { QuizItemsId = 3, IncorrectAnswersId = 9 },
                        // "8 : 2"
                        new { QuizItemsId = 4, IncorrectAnswersId = 2 },
                        new { QuizItemsId = 4, IncorrectAnswersId = 6 },
                        new { QuizItemsId = 4, IncorrectAnswersId = 8 },
                        // "1 * 1"
                        new { QuizItemsId = 5, IncorrectAnswersId = 3 },
                        new { QuizItemsId = 5, IncorrectAnswersId = 2 },
                        new { QuizItemsId = 5, IncorrectAnswersId = 4 },
                        // "6 * 6"
                        new { QuizItemsId = 6, IncorrectAnswersId = 3 },
                        new { QuizItemsId = 6, IncorrectAnswersId = 6 },
                        new { QuizItemsId = 6, IncorrectAnswersId = 7 }
                    )
                );
        }
    }
}
