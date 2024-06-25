using CarRental.Domain.Models;

namespace CarRental.Domain.Contracts
{
    public interface ICarRepository : IRepository<Car>
    {
        int GetMaxId();

    }
}