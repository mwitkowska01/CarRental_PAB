using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;

namespace CarRental.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRentalUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(OrderDto dto)
        {
            if (dto == null)
               throw new BadRequestException("Order is null");

            var id = _uow.OrderRepository.GetMaxId() + 1;
            var order = _mapper.Map<Order>(dto);
            order.Id = id;

            _uow.OrderRepository.Insert(order);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var order = _uow.OrderRepository.Get(id);
            if (order == null)
                throw new NotFoundException("Order not found");

            _uow.OrderRepository.Delete(order);
            _uow.Commit();
        }

        public List<OrderDto> GetAll()
        {
            var orders = _uow.OrderRepository.GetAll();

            List<OrderDto> result = _mapper.Map<List<OrderDto>>(orders);
            return result;
        }

        public OrderDto GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is less than zero");

            var order = _uow.OrderRepository.Get(id);
            if (order == null)
                throw new NotFoundException("Order not found");

            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        public void Update(OrderDto dto)
        {
            if (dto == null)
                throw new BadRequestException("No order data");

            var order = _uow.OrderRepository.Get(dto.Id);
            if (order == null)
                throw new NotFoundException("Order not found");

            _uow.Commit();
        }

        public void CompleteOrder(int id)
        {
            var order = _uow.OrderRepository.Get(id);
            if (order == null)
                throw new BadRequestException("Order not found");

            _uow.OrderRepository.CompleteOrder(id);
        }

        public void AddPersonel(int OrderId, int PersonelId)
        {
            var order = _uow.OrderRepository.Get(OrderId);
            if (order == null)
                throw new BadRequestException("Order not found");

            var personel = _uow.PersonelRepository.Get(PersonelId);

            if (personel == null)
                throw new BadRequestException("Personel not found");

            _uow.OrderRepository.AddPersonel(OrderId, PersonelId);

        }
    }
}

