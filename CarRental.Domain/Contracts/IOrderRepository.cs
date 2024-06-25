using CarRental.Domain.Models;

namespace CarRental.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetMaxId();
        void CompleteOrder(int id);
    }
}