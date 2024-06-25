using CarRental.Domain.Contracts;
using CarRental.Domain.Models;

namespace CarRental.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly RentalDbContext _rentalDbContext;
        public ServiceRepository(RentalDbContext context)
        : base(context)
        {
            _rentalDbContext = context;
        }
        public int GetMaxId()
        {
            return _rentalDbContext.Services.Max(x => x.Id);
        }
    }
}