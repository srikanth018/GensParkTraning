using System.Threading.Tasks;
using BCrypt.Net;
using ClosedXML.Excel;
using QuizApp.DTOs;
using QuizApp.Models;

namespace QuizApp.Misc
{
    public static class Generators
    {
        private static readonly Random _random = new Random();

        public static string GenerateID(string prefix)
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            int random = new Random().Next(0, 65536);
            return $"{prefix}{timestamp:X8}{random:X4}";
        }


        public static string GenerateHashedPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty");

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Password and hashed password cannot be null or empty");

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private static async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Invalid image file");
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Questions");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return relative path for public URL (served via wwwroot)
            return $"/uploads/questions/{fileName}";
        }

        public static async Task<string> GenerateImageFilePath(IFormFile imageFile)
        {
            if (imageFile == null || string.IsNullOrWhiteSpace(imageFile.FileName))
                throw new ArgumentException("Image file cannot be null or empty");

            return await UploadImageAsync(imageFile);
        }
        public static CreateQuizRequestDTO ParseQuizFromWorksheet(IXLWorksheet worksheet)
        {
            var quiz = new CreateQuizRequestDTO();
            quiz.Questions = new List<CreateQuestionDTO>();

            // Assume quiz-wide info is in first data row (row 2)
            var firstDataRow = worksheet.Row(2);

            quiz.Title = firstDataRow.Cell(1).GetValue<string>();
            quiz.Category = firstDataRow.Cell(2).GetValue<string>();
            quiz.UploadedBy = firstDataRow.Cell(3).GetValue<string>();
            quiz.TotalMarks = firstDataRow.Cell(4).GetValue<int>();

            int optionStartCol = 8;  // Option1 starts at column 8

            foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header
            {
                if (string.IsNullOrWhiteSpace(row.Cell(5).GetValue<string>())) continue; // Skip if QuestionText is empty

                var question = new CreateQuestionDTO
                {
                    QuestionText = row.Cell(5).GetValue<string>(),
                    Mark = row.Cell(6).GetValue<int>(),
                    Options = new List<CreateOptionDTO>()
                };

                // Parse options dynamically
                for (int i = optionStartCol; i <= row.LastCellUsed().Address.ColumnNumber; i += 2)
                {
                    var optionText = row.Cell(i).GetValue<string>();
                    var isCorrect = row.Cell(i + 1).GetValue<bool>();

                    if (!string.IsNullOrWhiteSpace(optionText))
                    {
                        question.Options.Add(new CreateOptionDTO
                        {
                            OptionText = optionText,
                            IsCorrect = isCorrect
                        });
                    }
                }

                quiz.Questions.Add(question);
            }

            return quiz;
        }

        public static int GenerateTotalMarksSecured(SubmitQuizRequestDTO submitedQuiz, Quiz quiz)
        {
            if (submitedQuiz == null)
                throw new ArgumentException("Quiz or questions cannot be null or empty");

            int TotalMarksSecured = 0;

            foreach (var question in submitedQuiz.Questions)
            {
                var quizQuestion = quiz.Questions?.FirstOrDefault(q => q.Id == question.QuestionId);
                if (quizQuestion != null)
                {
                    var correctOptions = quizQuestion.Options?.Where(o => o.IsCorrect).Select(o => o.Id).ToList();
                    if (correctOptions != null && question.SelectedOptionIds.All(id => correctOptions.Contains(id)))
                    {
                        TotalMarksSecured += quizQuestion.Mark;
                    }
                }
            }
            return TotalMarksSecured;
        }

        public static int GenerateCreditPoints(int totalMarksSecured, int totalMarks, int negativePoint=0)
        {
            if (totalMarksSecured < 0)
                throw new ArgumentException("Total marks secured cannot be negative");

            if (negativePoint < 0)
                throw new ArgumentException("Negative mark cannot be negative");

            if (totalMarksSecured < 0)
                totalMarksSecured = 0;

            if (totalMarks <= 0)
                return 0; 

            if (totalMarks <= 0)
                throw new ArgumentException("Total marks must be greater than zero");

            double percentage = (double)totalMarksSecured / totalMarks * 100;
            int creditPoints = (int)Math.Round(percentage / 10);
            creditPoints -= negativePoint; 
            return creditPoints;
        }
    }
}