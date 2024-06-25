
using CarRental.Domain.Contracts;
using CarRental.Domain.Models;

namespace CarRental.Infrastructure.Repositories
{
    public class PersonelRepository : Repository<Personel>, IPersonelRepository
    {
        private readonly RentalDbContext _rentalDbContext;
        public PersonelRepository(RentalDbContext context)
        : base(context)
        {
            _rentalDbContext = context;
        }

        public int GetMaxId()
        {
            return _rentalDbContext.Personels.Max(x => x.Id);
        }
    }
}