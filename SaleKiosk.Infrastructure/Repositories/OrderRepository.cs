using CarRental.Domain.Contracts;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly RentalDbContext _rentalDbContext;
        public OrderRepository(RentalDbContext context)
        : base(context)
        {
            _rentalDbContext = context;
        }
        public int GetMaxId()
        {
            return _rentalDbContext.Orders.Max(x => x.Id);
        }

        public override List<Order> GetAll()
        {
           return _rentalDbContext.Orders
                .Include(o => o.Details)
                .Include(o => o.Car)
                .Include(o => o.Personel)
                .Include(o => o.Contractor)
                .ToList();
        }

        public void CompleteOrder(int id)
        {
            var order = _rentalDbContext.Orders.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                order.CompletionDate = DateTime.Now;

                _rentalDbContext.SaveChanges();
            }
        }

        public void AddPersonel(int OrderId, int PersonelId)
        {
            var personel = _rentalDbContext.Personels.FirstOrDefault(o => o.Id == PersonelId);
            var order = _rentalDbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
            if (order != null && personel != null)
            {
                order.Personel = personel;
                _rentalDbContext.SaveChanges();
            }
        }
    }
}