using AutoMapper;
using CarRental.Application.IServices;
using CarRental.Domain.Contracts;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;

namespace CarRental.Application.Services
{
    public class PersonelService : IPersonelService
    {
        private readonly IRentalUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonelService(IRentalUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(PersonelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Product is null");
            }

            var id = _uow.PersonelRepository.GetMaxId() + 1;
            var car = _mapper.Map<Personel>(dto);
            car.Id = id;

            _uow.PersonelRepository.Insert(car);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var car = _uow.PersonelRepository.Get(id);
            if (car == null)
            {
                throw new NotFoundException("Product not found");
            }

            _uow.PersonelRepository.Delete(car);
            _uow.Commit();
        }

        public List<PersonelDto> GetAll()
        {
            var cars = _uow.PersonelRepository.GetAll();

            List<PersonelDto> result = _mapper.Map<List<PersonelDto>>(cars);
            return result;
        }
        public PersonelDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var car = _uow.PersonelRepository.Get(id);
            if (car == null)
            {
                throw new NotFoundException("Product not found");
            }

            var result = _mapper.Map<PersonelDto>(car);
            return result;
        }

        public void Update(PersonelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No car data");
            }

            var car = _uow.PersonelRepository.Get(dto.Id);
            if (car == null)
            {
                throw new NotFoundException("Product not found");
            }

            //car.LicensePlate = dto.LicensePlate;
            car.Phone = dto.Phone;
            car.Email = dto.Email;
            car.LastName = dto.LastName;
            car.Name = dto.Name;

            _uow.Commit();
        }
    }
}
