namespace Domain.DTO
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ContactPerson { get; set; }
        public int? ContactPersonNumber { get; set; }

    }
}
