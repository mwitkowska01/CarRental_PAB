namespace CarRental.SharedKernel.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<ServiceDto> Services { get; set; }
        public CarDto Car { get; set; }
        public PersonelDto Personel { get; set; }
    }
}