using CarRental.SharedKernel.Dto;

namespace CarRental.Application.IServices
{
    public interface ICarService
    {
        List<CarDto> GetAll();
        CarDto GetById(int id);
        int Create(CarDto dto);
        void Update(CarDto dto);
        void Delete(int id);
    }
}
