using Comfy.Product.ViewModel;
using FluentValidation;
using System;

namespace Comfy.API.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleViewModel>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.Date)
                .NotNull()
                .GreaterThan(DateTime.Now)
                .WithMessage("Scheduling date must be over now");

            RuleFor(x => x.ProcedurePerformed)
                .NotNull()
                .WithMessage("Procedure field is required");
        }
    }
}
