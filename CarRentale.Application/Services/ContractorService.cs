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
                throw new BadRequestException("Contractor is null");

            var id = _uow.ContractorRepository.GetMaxId() + 1;
            var contractor = _mapper.Map<Contractor>(dto);
            contractor.Id = id;
            _uow.ContractorRepository.Insert(contractor);

            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var contractor = _uow.ContractorRepository.Get(id);

            if (contractor == null)
                throw new NotFoundException("Contractor not found");

            _uow.ContractorRepository.Delete(contractor);
            _uow.Commit();
        }

        public List<ContractorDto> GetAll()
        {
            var contractors = _uow.ContractorRepository.GetAll();

            List<ContractorDto> result = _mapper.Map<List<ContractorDto>>(contractors);
            return result;
        }
        public ContractorDto GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is less than zero");

            var contractor = _uow.ContractorRepository.Get(id);

            if (contractor == null)
                throw new NotFoundException("Contractor not found");

            var result = _mapper.Map<ContractorDto>(contractor);

            return result;
        }

        public void Update(ContractorDto dto)
        {
            if (dto == null)
                throw new BadRequestException("No Contractor data");

            var contractor = _uow.ContractorRepository.Get(dto.Id);

            if (contractor == null)
                 throw new NotFoundException("Contractor not found");

            contractor.Address = dto.Address;
            contractor.PhoneNumber = dto.PhoneNumber;
            contractor.LastName = dto.LastName;
            contractor.Email = dto.Email;

            _uow.Commit();
        }

        public List<CarDto> GetContractorCar(int id)
        {
            List<Car> contractorList = _uow.ContractorRepository.GetContractorCar(id);

            if (contractorList == null)
                throw new NotFoundException("List contractor not found");

            List<CarDto> result = _mapper.Map<List<CarDto>>(contractorList);

            return result;
        }

        public void DeleteCar(int contractorId, int carId)
        {
            var contracotr = _uow.ContractorRepository.Get(contractorId);

            if (contracotr == null)
                throw new NotFoundException("Contractor not found");

            var contractor = _uow.CarRepository.Get(carId);

            if (contractor == null)
                throw new NotFoundException("Contractor not found");

            _uow.ContractorRepository.DeleteCar(contractorId, carId);
        }
    }
}
