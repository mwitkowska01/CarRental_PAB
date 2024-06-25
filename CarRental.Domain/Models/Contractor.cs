namespace CarRental.Domain.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
