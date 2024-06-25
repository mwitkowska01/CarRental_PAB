using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
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
            {
                throw new BadRequestException("Service is null");
            }

            var id = _uow.ServiceRepository.GetMaxId() + 1;
            var car = _mapper.Map<Service>(dto);
            car.Id = id;

            _uow.ServiceRepository.Insert(car);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var car = _uow.ServiceRepository.Get(id);
            if (car == null)
            {
                //    throw new NotFoundException("Product not found");
            }

            _uow.ServiceRepository.Delete(car);
            _uow.Commit();
        }

        public List<ServiceDto> GetAll()
        {
            var cars = _uow.ServiceRepository.GetAll();

            List<ServiceDto> result = _mapper.Map<List<ServiceDto>>(cars);
            return result;
        }
        public ServiceDto GetById(int id)
        {
            if (id <= 0)
            {
                //throw new BadRequestException("Id is less than zero");
            }

            var car = _uow.ServiceRepository.Get(id);
            if (car == null)
            {
                //throw new NotFoundException("Product not found");
            }

            var result = _mapper.Map<ServiceDto>(car);
            return result;
        }

        public void Update(ServiceDto dto)
        {
            if (dto == null)
            {
                //throw new BadRequestException("No car data");
            }

            var car = _uow.ServiceRepository.Get(dto.Id);
            if (car == null)
            {
                // throw new NotFoundException("Product not found");
            }

            //car.LicensePlate = dto.LicensePlate;

            _uow.Commit();
        }
    }
}