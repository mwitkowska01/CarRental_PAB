using CarRental.SharedKernel.Dto;

namespace CarRental.Application.IServices
{
    public interface IPersonelService
    {
        List<PersonelDto> GetAll();
        PersonelDto GetById(int id);
        int Create(PersonelDto dto);
        void Update(PersonelDto dto);
        void Delete(int id);
    }
}
