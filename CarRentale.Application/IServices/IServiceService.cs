using CarRental.SharedKernel.Dto;

namespace CarRental.Application.IServices
{
    public interface IServiceService
    {
        List<ServiceDto> GetAll();
        ServiceDto GetById(int id);
        int Create(ServiceDto dto);
        void Update(ServiceDto dto);
        void Delete(int id);
    }
}
