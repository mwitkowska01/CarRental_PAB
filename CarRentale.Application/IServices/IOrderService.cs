using CarRental.SharedKernel.Dto;

namespace CarRental.Application.IServices
{
    public interface IOrderService 
    {
        List<OrderDto> GetAll();
        OrderDto GetById(int id);
        int Create(OrderDto dto);
        void Update(OrderDto dto);
        void Delete(int id);
    }
}