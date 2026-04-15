using Api.Filters;
using Api.Validators;
using FluentValidation;

namespace Api.Registers.Validators;

public static class LoadValidators
{
    public static void Load(IServiceCollection services)
    {
        services.AddMvc(options => options.Filters.Add(new ModelValidationFilter()));
        services.AddValidatorsFromAssemblyContaining<ScheduleValidator>();
    }
}