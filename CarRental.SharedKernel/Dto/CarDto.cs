namespace CarRental.SharedKernel.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public CarMake Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
    }

    public enum CarMake
    {
        Toyota,
        Honda,
        Ford,
        Chevrolet,
        Nissan,
        BMW,
        MercedesBenz,
        Volkswagen,
        Audi,
        Hyundai,
        Kia,
        Mazda,
        Subaru,
        Lexus,
        Jaguar,
        LandRover,
        Porsche,
        Tesla
    }
}

