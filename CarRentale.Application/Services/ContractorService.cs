using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;

namespace CarRental.Application.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IRentalUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ContractorService(IRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(ContractorDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Contractor is null");
            }

            var id = _uow.ContractorRepository.GetMaxId() + 1;
            var car = _mapper.Map<Contractor>(dto);
            car.Id = id;
            _uow.ContractorRepository.Insert(car);

            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var car = _uow.ContractorRepository.Get(id);
            if (car == null)
            {
                //    throw new NotFoundException("Product not found");
            }

            _uow.ContractorRepository.Delete(car);
            _uow.Commit();
        }

        public List<ContractorDto> GetAll()
        {
            var cars = _uow.ContractorRepository.GetAll();

            List<ContractorDto> result = _mapper.Map<List<ContractorDto>>(cars);
            return result;
        }
        public ContractorDto GetById(int id)
        {
            if (id <= 0)
            {
                //throw new BadRequestException("Id is less than zero");
            }

            var car = _uow.ContractorRepository.Get(id);
            if (car == null)
            {
                //throw new NotFoundException("Product not foun     d");
            }

            var result = _mapper.Map<ContractorDto>(car);
            return result;
        }

        public void Update(ContractorDto dto)
        {
            if (dto == null)
            {
                //throw new BadRequestException("No car data");
            }

            var car = _uow.ContractorRepository.Get(dto.Id);
            if (car == null)
            {
                // throw new NotFoundException("Product not found");
            }

            //car.LicensePlate = dto.LicensePlate;

            _uow.Commit();
        }

        public List<CarDto> GetContractorCar(int id)
        {
            List<Car> carList = _uow.ContractorRepository.GetContractorCar(id);
            if (carList == null)
            {
                throw new NotFoundException("List car not found");
            }
            List<CarDto> result = _mapper.Map<List<CarDto>>(carList);

            return result;
        }
    }
}
