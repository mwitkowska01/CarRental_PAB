using CarRental.Domain.Contracts;
using CarRental.SharedKernel.Dto;
using FluentValidation;

namespace CarRental.Application.Validators
{
    public class RegisterServiceDtoValidator : AbstractValidator<ServiceDto>
    {
        public RegisterServiceDtoValidator(IRentalUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(60);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(120);

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
