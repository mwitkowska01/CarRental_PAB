using CarRental.Domain.Models;

namespace CarRental.Domain.Contracts
{
    public interface IServiceRepository : IRepository<Service>
    {
        int GetMaxId();

    }
}
