using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.EF.Entities;
using Infrastructure.EF.Mappers;
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
        private readonly IMapper _mapper;
        public QuizUserServiceEF(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> FindAllQuizzes()
        {
            var quizzes = _context.Quizzes
                .Include(q => q.Items)
                    .ThenInclude(i => i.IncorrectAnswers)
                .AsNoTracking()
                .ToList();

            return quizzes.Select(QuizMapper.FromEntityToQuiz);
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context.Quizzes
                .Include(q => q.Items)
                    .ThenInclude(i => i.IncorrectAnswers)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);

            return entity != null ? QuizMapper.FromEntityToQuiz(entity) : null;
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            var userAnswers = _context.UserAnswers
                .Where(ua => ua.QuizId == quizId && ua.UserId == userId)
                .ToList();

            var quizItemUserAnswers = userAnswers
                .Select(QuizMapper.FromEntityToQuizItemUserAnswer)
                .ToList();

            return quizItemUserAnswers;
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
        {
            /*var quizzEntity = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);
            if (quizzEntity is null)
            {
                throw new QuizNotFoundException($"Quiz with id {quizId} not found");
            }

            var item = _context.QuizItems.FirstOrDefault(qi => qi.Id == quizItemId);
            if (item is null)
            {
                throw new QuizItemNotFoundException($"Quiz item with id {quizItemId} not found");
            }*/

            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                UserId = userId,
                QuizItemId = quizItemId,
                QuizId = quizId,
                UserAnswer = answer
            };
            try
            {
                var saved = _context.UserAnswers.Add(entity).Entity;
                _context.SaveChanges();
                return new QuizItemUserAnswer()
                {
                    UserId = saved.UserId,
                    QuizItem = QuizMapper.FromEntityToQuizItem(saved.QuizItem),
                    QuizId = saved.QuizId,
                    Answer = saved.UserAnswer
                };
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.StartsWith("The INSERT"))
                {
                    throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
                }
                if (e.InnerException.Message.StartsWith("Violation of"))
                {
                    throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
                }
                throw new Exception(e.Message);
            }

            /*var savedEntity = _context.Add(entity).Entity;
            _context.SaveChanges();


            var quizItemUserAnswer = new QuizItemUserAnswer()
            {
                QuizId = savedEntity.QuizId,
                QuizItem = _mapper.Map<QuizItem>(savedEntity.QuizItem),
                UserId = savedEntity.UserId,
                Answer = savedEntity.UserAnswer
            };

            return quizItemUserAnswer;*/


            //////////////////////////////////////////////////

            

        }
    }
}
