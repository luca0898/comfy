using Comfy.API.Middlewares;
using Comfy.API.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Registers.Validators
{
    public static class LoadValidators
    {
        public static void Load(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new ModelValidationFilter()));
            services.AddValidatorsFromAssemblyContaining<ScheduleValidator>();
        }
    }
}