using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using QuizApp.Models;

namespace QuizApp.Misc
{
    public class CustomException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var errorId = Guid.NewGuid();
            var request = context.HttpContext.Request;

            if (exception is InvalidOperationException)
            {
                Log.Warning(exception, 
                    "Bad request | ErrorID: {ErrorID} | Path: {Path} | Method: {Method}",
                    errorId, request.Path, request.Method);
                
                context.Result = new BadRequestObjectResult(new CustomError
                {
                    Message = exception.Message,
                    StatusCode = 400,
                    ErrorId = errorId
                });
            }
            else if (exception is KeyNotFoundException)
            {
                Log.Warning(exception, 
                    "Resource not found | ErrorID: {ErrorID} | Path: {Path} | Method: {Method}",
                    errorId, request.Path, request.Method);
                
                context.Result = new NotFoundObjectResult(new CustomError
                {
                    Message = exception.Message,
                    StatusCode = 404,
                    ErrorId = errorId
                });
            }
            else
            {
                Log.Error(exception, 
                    "Unhandled exception | ErrorID: {ErrorID} | Path: {Path} | Method: {Method}",
                    errorId, request.Path, request.Method);
                
                context.Result = new ObjectResult(new CustomError
                {
                    Message = $"An error occurred. Reference: {errorId}",
                    StatusCode = 500,
                    ErrorId = errorId
                })
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}