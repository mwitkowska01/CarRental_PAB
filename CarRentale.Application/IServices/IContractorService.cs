using CarRental.SharedKernel.Dto;

namespace CarRental.Application.IServices
{
    public interface IContractorService
    {
        List<ContractorDto> GetAll();
        ContractorDto GetById(int id);
        int Create(ContractorDto dto);
        void Update(ContractorDto dto);
        void Delete(int id);
        List<CarDto> GetContractorCar(int id);
    }
}
