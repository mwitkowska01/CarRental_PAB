using CarRental.SharedKernel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
