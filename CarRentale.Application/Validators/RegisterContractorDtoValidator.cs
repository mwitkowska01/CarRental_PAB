using CarRental.Domain.Contracts;
using CarRental.SharedKernel.Dto;
using FluentValidation;

namespace CarRental.Application.Validators
{
    public class RegisterContractorDtoValidator : AbstractValidator<ContractorDto>
    {
        public RegisterContractorDtoValidator(IRentalUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(2, 30);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .Length(2, 30);


            RuleFor(p => p.PhoneNumber)
                .Length(9)
                .NotEmpty();

            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(5, 50);

            RuleFor(p => p.Address)
                .NotEmpty()
                .Length(2, 120);

        }
    }
}
