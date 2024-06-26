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
                throw new BadRequestException("Personel is null");

            var id = _uow.PersonelRepository.GetMaxId() + 1;
            var personel = _mapper.Map<Personel>(dto);
            personel.Id = id;

            _uow.PersonelRepository.Insert(personel);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var personel = _uow.PersonelRepository.Get(id);
            if (personel == null)
                throw new NotFoundException("Personel not found");

            _uow.PersonelRepository.Delete(personel);
            _uow.Commit();
        }

        public List<PersonelDto> GetAll()
        {
            var personels = _uow.PersonelRepository.GetAll();

            List<PersonelDto> result = _mapper.Map<List<PersonelDto>>(personels);
            return result;
        }
        public PersonelDto GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is less than zero");

            var personel = _uow.PersonelRepository.Get(id);

            if (personel == null)
                throw new NotFoundException("Personel not found");

            var result = _mapper.Map<PersonelDto>(personel);
            return result;
        }

        public void Update(PersonelDto dto)
        {
            if (dto == null)
                throw new BadRequestException("No personel data");

            var personel = _uow.PersonelRepository.Get(dto.Id);

            if (personel == null)
                throw new NotFoundException("Personel not found");

            personel.Phone = dto.Phone;
            personel.Email = dto.Email;
            personel.LastName = dto.LastName;
            personel.Name = dto.Name;

            _uow.Commit();
        }
    }
}
