using CarRental.Domain.Contracts;
using CarRental.Domain.Models;

namespace CarRental.Infrastructure.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly RentalDbContext _rentalDbContext;
        public CarRepository(RentalDbContext context)
        : base(context)
        {
            _rentalDbContext = context;
        }
        public int GetMaxId()
        {
            return _rentalDbContext.Cars.Max(x => x.Id);
        }
    }
}