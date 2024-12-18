namespace TechChallange.Api.Controllers.Region.Dto
{
    public record RegionCreateDto
    {
        public string Name { get; init; }
        public string Ddd { get; init; }
    }
}
