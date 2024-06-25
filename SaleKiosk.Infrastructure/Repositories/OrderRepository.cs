using CarRental.Domain.Contracts;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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
    }
}