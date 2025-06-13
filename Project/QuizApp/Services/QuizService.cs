using Microsoft.AspNetCore.SignalR;
using QuizApp.DTOs;
using QuizApp.Hubs;
using QuizApp.Interfaces;
using QuizApp.Mappers;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly IRepository<string, Quiz> _quizRepository;
        private readonly ITeacherService _teacherService;
        // private readonly IHubContext<QuizHub> _hubContext;


        public QuizService(IRepository<string, Quiz> quizRepository,
                           ITeacherService teacherService
                        //    IHubContext<QuizHub> hubContext
                           )
        {
            _quizRepository = quizRepository;
            _teacherService = teacherService;
            // _hubContext = hubContext;
        }

        public async Task<Quiz> CreateQuizAsync(CreateQuizRequestDTO quiz)
        {
            var teacher = await _teacherService.GetByEmailAsync(quiz.UploadedBy);
            if (teacher == null)
            {
                throw new KeyNotFoundException("Teacher not found");
            }

            var quizEntity = await QuizMappers.CreateQuiz(quiz);
            var createdQuiz = await _quizRepository.Add(quizEntity);

            // await _hubContext.Clients.All.SendAsync("ReceiveNewQuiz", createdQuiz.Category, createdQuiz.Title);

            return createdQuiz;
        }
        public async Task<Quiz?> GetQuizByIdAsync(string id)
        {
            var quiz = await _quizRepository.GetById(id);
            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _quizRepository.GetAll();
        }

        public async Task<Quiz> DeleteQuizAsync(string id)
        {
            var quiz = await _quizRepository.GetById(id);
            if (quiz != null)
            {
                return await _quizRepository.Delete(quiz);
            }
            throw new KeyNotFoundException("Quiz not found");
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByTeacherEmailAsync(string email)
        {
            var quizzes = await _quizRepository.GetAll();
            return quizzes.Where(q => q.UploadedBy == email);
        }

    }
}