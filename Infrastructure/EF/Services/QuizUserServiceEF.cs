using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;

        public QuizUserServiceEF(QuizDbContext dbContext)
        {
            _context = dbContext;
        }
        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> FindAllQuizzes()
        {
            return _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .Select(QuizMapper.FromEntityToQuiz)    //dorobić mappery
                .ToList();
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : QuizMapper.FromEntityToQuiz(entity);     // dorobić mappery
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            throw new NotImplementedException();
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
        {
            var quizzEntity = ... // pobierz encję quizu o quizId
        if (quizzEntity is null)
            {
                throw new QuizNotFoundException($"Quiz with id {quizId} not found");
            }
            var item = ... // pobierz encję elementu quizu o quizItemId 
        if (item is null)
            {
                throw new QuizItemNotFoundException($"Quiz item with id {quizId} not found");
            }
            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                QuizId = quizId,
                UserAnswer = answer,
                UserId = userId,
                QuizItemId = quizItemId
            };
            var savedEntity = _context.Add(entity).Entity;
            _context.SaveChanges();
            return new QuizItemUserAnswer()
        {
            ...  // uzupełnij aby zmapować obiekt entity na klasę QuizItemUserAnswer 
        };
        }
    }
}
