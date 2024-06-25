namespace CarRental.SharedKernel.Dto
{
    public class ContractorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CarDto> Cars { get; set; }
    }
}
