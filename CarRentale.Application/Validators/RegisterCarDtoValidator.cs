using CarRental.Domain.Contracts;
using CarRental.SharedKernel.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Validators
{
    internal class RegisterCarDtoValidator : AbstractValidator<CarDto>
    {
        public RegisterCarDtoValidator(IRentalUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Model)
                .NotEmpty()
                .Length(2, 20);

            RuleFor(p => p.LicensePlate)
                .NotEmpty()
                .Length(4, 8);

            RuleFor(p => p.Year)
                .NotEmpty()
                .GreaterThan(1900)
                .LessThan(DateTime.Now.Year+1);

            RuleFor(p => p.VIN)
                .NotEmpty().WithMessage("Numer VIN jest wymagany.")
                .Length(17).WithMessage("Numer VIN musi mieć dokładnie 17 znaków.")
                .Must(BeAValidVin).WithMessage("Numer VIN może zawierać tylko cyfry i litery (bez liter I, O, Q).");
        }

        private bool BeAValidVin(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                return false;

            if (vin.Length != 17)
                return false;

            foreach (char c in vin)
            {
                if (!char.IsLetterOrDigit(c) || "IOQioq".Contains(c))
                    return false;
            }

            return true;
        }
    }
}
