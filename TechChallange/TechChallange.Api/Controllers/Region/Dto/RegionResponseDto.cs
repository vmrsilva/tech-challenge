﻿namespace TechChallange.Api.Controllers.Region.Dto
{
    public record RegionResponseDto
    {
        public  Guid Id { get; init; }
        public string Name { get; init; }
        public string Ddd { get; init; }
    }
}