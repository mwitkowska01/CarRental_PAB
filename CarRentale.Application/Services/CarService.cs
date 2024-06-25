using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;

namespace CarRental.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IRentalUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CarService(IRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CarDto dto)
        {
            if (dto == null)
            {
                //throw new BadRequestException("Product is null");
            }

            var id = _uow.CarRepository.GetMaxId() + 1;
            var car = _mapper.Map<Car>(dto);
            car.Id = id;

            _uow.CarRepository.Insert(car);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var car = _uow.CarRepository.Get(id);
            if (car == null)
            {
            //    throw new NotFoundException("Product not found");
            }

            _uow.CarRepository.Delete(car);
            _uow.Commit();
        }

        public List<CarDto> GetAll()
        {
            var cars = _uow.CarRepository.GetAll();

            List<CarDto> result = _mapper.Map<List<CarDto>>(cars);
            return result;
        }
        public CarDto GetById(int id)
        {
            if (id <= 0)
            {
                //throw new BadRequestException("Id is less than zero");
            }

            var car = _uow.CarRepository.Get(id);
            if (car == null)
            {
                //throw new NotFoundException("Product not found");
            }

            var result = _mapper.Map<CarDto>(car);
            return result;
        }

        public void Update(CarDto dto)
        {
            if (dto == null)
            {
                //throw new BadRequestException("No car data");
            }

            var car = _uow.CarRepository.Get(dto.Id);
            if (car == null)
            {
               // throw new NotFoundException("Product not found");
            }

            car.LicensePlate = dto.LicensePlate;
            car.Year = dto.Year;
            car.Make = (Domain.Models.CarMake)dto.Make;
            car.LicensePlate = dto.LicensePlate;


            _uow.Commit();
        }
    }
}
