using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using product_service.ports.dtos.settings;
using product_service.ports.shared.dictionaries;
using product_service.ports.shared.enums;

namespace product_service.adapters.input.filters
{
    public class ValidationFilter<Response> : IAsyncActionFilter where Response : class
    {
        private readonly IValidator<Response> _validator;

        public ValidationFilter(IValidator<Response> validator)
        {
            _validator = validator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var argument = context.ActionArguments.Values.OfType<Response>().FirstOrDefault();
            if (argument == null)
            {
                context.Result = new BadRequestObjectResult(new ApiResponse<object>(
                    false, "Solicitud inválida o cuerpo vacío.", null, (int)ApiMessage.BadRequest));
                return;
            }

            var validationResult = await _validator.ValidateAsync(argument);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => string.IsNullOrEmpty(e.PropertyName) ? "general" : e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = new ApiResponse<object>(
                    false,
                    ApiMessage.ValidationError.GetMessage(),
                    errors,
                    (int)ApiMessage.ValidationError
                );

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();

        }
    }
}
