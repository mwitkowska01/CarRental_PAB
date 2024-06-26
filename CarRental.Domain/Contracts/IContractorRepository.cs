using CarRental.Domain.Models;

namespace CarRental.Domain.Contracts
{
    public interface IContractorRepository : IRepository<Contractor>
    {
        int GetMaxId();
        List<Car> GetContractorCar(int id);
        void DeleteCar(int contractorId, int carId);

    }
}
