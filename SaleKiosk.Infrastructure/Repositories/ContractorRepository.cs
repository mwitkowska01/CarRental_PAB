using CarRental.Domain.Contracts;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class ContractorRepository : Repository<Contractor>, IContractorRepository
    {
        private readonly RentalDbContext _rentalDbContext;
        public ContractorRepository(RentalDbContext context)
        : base(context)
        {
            _rentalDbContext = context;
        }
        public int GetMaxId()
        {
            return _rentalDbContext.Contractors.Max(x => x.Id);
        }

        public List<Car> GetContractorCar(int id)
        {
            List<Car> carList = new List<Car>();

            carList = _rentalDbContext.Contractors
                 .Where(c => c.Id == id)
                 .SelectMany(c => c.Cars)
                 .ToList();


            var kk = _rentalDbContext.Contractors.ToList();


            return carList;
        }

        public void DeleteCar(int contractorId, int carId)
        {
            var contractor = _rentalDbContext.Contractors
                .Include(c => c.Cars)  
                .FirstOrDefault(c => c.Id == contractorId);

            if (contractor != null)
            {
                var carToDelete = contractor.Cars.FirstOrDefault(c => c.Id == carId);
                if (carToDelete != null)
                {
                    contractor.Cars.Remove(carToDelete);

                    _rentalDbContext.SaveChanges();
                }
            }
        }
    }
}