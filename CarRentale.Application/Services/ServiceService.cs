using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;
using CarRental.Domain.Exceptions;

namespace CarRental.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IRentalUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ServiceService(IRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(ServiceDto dto)
        {
            if (dto == null)
                throw new BadRequestException("Service is null");

            var id = _uow.ServiceRepository.GetMaxId() + 1;
            var service = _mapper.Map<Service>(dto);
            service.Id = id;

            _uow.ServiceRepository.Insert(service);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var service = _uow.ServiceRepository.Get(id);
            if (service == null)
                throw new NotFoundException("Services not found");

            _uow.ServiceRepository.Delete(service);
            _uow.Commit();
        }

        public List<ServiceDto> GetAll()
        {
            var services = _uow.ServiceRepository.GetAll();

            List<ServiceDto> result = _mapper.Map<List<ServiceDto>>(services);
            return result;
        }
        public ServiceDto GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is less than zero");

            var service = _uow.ServiceRepository.Get(id);

            if (service == null)
                throw new NotFoundException("Services not found");

            var result = _mapper.Map<ServiceDto>(service);
            return result;
        }

        public void Update(ServiceDto dto)
        {
            if (dto == null)
                throw new BadRequestException("No Services data");

            var service = _uow.ServiceRepository.Get(dto.Id);

            if (service == null)
                 throw new NotFoundException("Services not found");

            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Price = dto.Price;

            _uow.Commit();
        }
    }
}