namespace CarRental.Domain.Models
{
    public class Personel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Position Position { get; set; }
    }
    public enum Position
    {
        employee,
        ShiftSupervisor,
        Manager,
        Owner
    }
}

