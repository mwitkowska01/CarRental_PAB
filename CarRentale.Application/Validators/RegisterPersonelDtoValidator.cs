using CarRental.Domain.Contracts;
using CarRental.SharedKernel.Dto;
using FluentValidation;

namespace CarRental.Application.Validators
{
    public class RegisterPersonelDtoValidator : AbstractValidator<PersonelDto>
    {
        public RegisterPersonelDtoValidator(IRentalUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(2, 30);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .Length(2, 30);


            RuleFor(p => p.Phone)
                .Length(9)
                .NotEmpty();

            RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Nieprawidłowy adres e-mail.")
            .Length(5, 50);
        }
    }
}
