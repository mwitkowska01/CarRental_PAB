using AutoMapper;
using CarRental.Domain.Models;
using CarRental.SharedKernel.Dto;

namespace CarRental.Application.Mapping
{
    public class RentalMappingProfile : Profile
    {
        public RentalMappingProfile() 
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();

            CreateMap<Contractor, ContractorDto>();
            CreateMap<ContractorDto, Contractor>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<Personel, PersonelDto>();
            CreateMap<PersonelDto, Personel>();

            CreateMap<Service, ServiceDto>();
            CreateMap<ServiceDto, Service>();
        }
    }
}