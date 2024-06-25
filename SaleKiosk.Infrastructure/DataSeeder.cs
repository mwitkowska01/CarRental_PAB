using CarRental.Domain.Models;
using System.Xml.Linq;

namespace CarRental.Infrastructure
{
    public class DataSeeder
    {
        private readonly RentalDbContext _dbContext;

        public DataSeeder(RentalDbContext context)
        {
            this._dbContext = context;
        }

        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Cars.Any())
                {
                    var cars = new List<Car>
                    {
                        new Car()
                        {
                            Id = 1,
                            Make = CarMake.Kia,
                            Model = "Sorento",
                            LicensePlate = "KR12345",
                            VIN = "KNAJC8114L7061935",
                            Year = 2019
                        },
                        new Car()
                        {
                            Id = 2,
                            Make = CarMake.Audi,
                            Model = "A4",
                            LicensePlate = "WA98765",
                            VIN = "WAUZZZ8V3KA106783",
                            Year = 2010
                        },
                        new Car()
                        {
                            Id = 3,
                            Make = CarMake.Toyota,
                            Model = "Corolla",
                            LicensePlate = "GD65432",
                            VIN = "JTDBU4EE9AJ042378",
                            Year = 2020
                        }
                    };
                    _dbContext.Cars.AddRange(cars);
                }

                _dbContext.SaveChanges();

                if (!_dbContext.Contractors.Any())
                {
                    var contractors = new List<Contractor>
                    {
                        new Contractor()
                        {
                            Id = 1,
                            Name = "Anna",
                            LastName = "Gawłowska",
                            Address = "Kraków, Czarnowiejska 15A",
                            PhoneNumber = "123 123 123",
                            Email = "ag@gmail.com",
                            Cars = _dbContext.Cars.Take(2).ToList()
                        },
                        new Contractor()
                        {
                            Id = 2,
                            Name = "Jan",
                            LastName = "Kowalski",
                            Address = "Warszawa, Marszałkowska 10",
                            PhoneNumber = "321 321 321",
                            Email = "jk@example.com",
                            Cars = _dbContext.Cars.Skip(2).Take(1).ToList()
                        }
                    };
                    _dbContext.Contractors.AddRange(contractors);
                }

                _dbContext.SaveChanges();

                if (!_dbContext.Personels.Any())
                {
                    var personnels = new List<Personel>
                    {
                        new Personel()
                        {
                            Id = 1,
                            Name = "Kamil",
                            LastName = "Grzyb",
                            Email = "k.grzyb@company.com",
                            Phone = "123456789",
                            Position = Position.Manager
                        },
                        new Personel()
                        {
                            Id = 2,
                            Name = "Monika",
                            LastName = "Nowak",
                            Email = "m.nowak@company.com",
                            Phone = "987654321",
                            Position = Position.Owner
                        }
                    };
                    _dbContext.Personels.AddRange(personnels);
                }

                _dbContext.SaveChanges();

                if (!_dbContext.Services.Any())
                {
                    var services = new List<Service>()
                    {
                        new Service()
                        {
                            Id = 1,
                            Name = "Wymiana oleju",
                            Description = "Kompleksowa wymiana oleju silnikowego",
                            Price = 150.00m
                        },
                        new Service()
                        {
                            Id = 2,
                            Name = "Wymiana silnika",
                            Description = "Wymiana jednostki napędowej",
                            Price = 5000.00m
                        },
                        new Service()
                        {
                            Id = 3,
                            Name = "Regulacja hamulca",
                            Description = "Regulacja układu hamulcowego",
                            Price = 200.00m
                        },
                        new Service()
                        {
                            Id = 4,
                            Name = "Wymiana opon",
                            Description = "Zmiana opon na nowe",
                            Price = 100.00m
                        }
                    };
                    _dbContext.Services.AddRange(services);
                }

                _dbContext.SaveChanges();

                if (!_dbContext.Orders.Any())
                {
                    var car1 = _dbContext.Cars.FirstOrDefault(c => c.Id == 1);
                    var car2 = _dbContext.Cars.FirstOrDefault(c => c.Id == 2);
                    var personnel1 = _dbContext.Personels.FirstOrDefault(p => p.Id == 1);
                    var personnel2 = _dbContext.Personels.FirstOrDefault(p => p.Id == 2);
                    var services1 = _dbContext.Services.Take(2).ToList();
                    var services2 = _dbContext.Services.Skip(2).Take(2).ToList();

                    if (car1 != null && personnel1 != null && services1.Any())
                    {
                        var order1 = new Order()
                        {
                            Id = 1,
                            AdmissionDate = DateTime.Now.AddDays(-3),
                            CompletionDate = DateTime.Now,
                            Car = car1,
                            Personel = personnel1,
                        };

                        var order2 = new Order()
                        {
                            Id = 2,
                            AdmissionDate = DateTime.Now.AddDays(-5),
                            Car = car2,
                            Personel = personnel2,
                        };

                        _dbContext.Orders.AddRange(order1, order2);
                    }
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}

