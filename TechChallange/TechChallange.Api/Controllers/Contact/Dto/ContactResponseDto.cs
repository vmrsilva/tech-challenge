namespace TechChallange.Api.Controllers.Contact.Dto
{
    public record ContactResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid RegionId { get; set; }
    }
}
