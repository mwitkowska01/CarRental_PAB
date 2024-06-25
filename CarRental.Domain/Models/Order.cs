namespace CarRental.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
        public Car Car { get; set; }
        public Personel Personel { get; set; }
    }
}