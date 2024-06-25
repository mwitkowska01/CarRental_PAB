using CarRental.Domain.Models;

namespace CarRental.Domain.Contracts
{
    public interface IPersonelRepository : IRepository<Personel>
    {
        int GetMaxId();

    }
}
