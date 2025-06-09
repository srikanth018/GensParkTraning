using Microsoft.EntityFrameworkCore;
using QuizApp.Contexts;
using QuizApp.Models;

namespace QuizApp.Repositories
{
    public class QuizRepository : Repository<string, Quiz>
    {
        public QuizRepository(QuizAppContext context) : base(context)
        {
        }

        public override async Task<ICollection<Quiz>> GetAll()
        {
            return await _quizAppContext.Quizzes.ToListAsync();
        }

        public override async Task<Quiz> GetById(string key)
        {
            var quiz = await _quizAppContext.Quizzes.FirstOrDefaultAsync(q => q.Id == key);
            if (quiz == null) throw new Exception("Quiz not found with the key");
            return quiz;
        }

    }
    
}