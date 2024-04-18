using System.Diagnostics.CodeAnalysis;

namespace WebAPI.Dto
{
    public class QuizItemUserAnswersDto
    {
        public int QuizItemId { get; set; }
        public bool IsCorrect { get; set; }

        public string Answer { get; set; }
        public string Question { get; set; }
    }
}
