using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EF.Mappers
{
    public static class QuizMapper
    {
        public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
        {
            return new QuizItem(
                entity.Id,
                entity.Question,
                entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
                entity.CorrectAnswer
            );
        }

        public static Quiz FromEntityToQuiz(QuizEntity entity)
        {
            var quizItems = entity.Items.Select(FromEntityToQuizItem).ToList();

            return new Quiz(
                entity.Id,
                quizItems,
                entity.Title
            );
        }

        public static QuizItemUserAnswer FromEntityToQuizItemUserAnswer(QuizItemUserAnswerEntity entity)
        {

            if (entity == null)
            {
                return null;
            }


            return new QuizItemUserAnswer
            {
                QuizId = entity.QuizId,
                QuizItem = FromEntityToQuizItem(entity.QuizItem),
                UserId = entity.UserId,
                Answer = entity.UserAnswer
            };
        }

    }

}
