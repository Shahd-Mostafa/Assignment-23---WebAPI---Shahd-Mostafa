using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Assignment_23___WebAPI___Shahd_Mostafa.Factories
{
    public class ApiresponseFactory
    {
        public static IActionResult CustomValidationError(ActionContext context)
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Any() ?? false)
                .Select(e => new validationError
                {
                    Field = e.Key,
                    Errors = e.Value?.Errors.Select(e => e.ErrorMessage).ToList()?? new List<string>()
                });
            var response= new ValidationErrorResult()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation Error",
                Errors = errors.ToList()
            };
            return new BadRequestObjectResult(response);
        }
    }
}
