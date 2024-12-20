﻿using FluentValidation;
using TechChallange.Api.Controllers.Region.Dto;

namespace TechChallange.Api.Validators.Region
{
    public class RegionCreateValidator : AbstractValidator<RegionCreateDto>
    {
        public RegionCreateValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome não pode exceder 50 caracteres.");

            RuleFor(c => c.Ddd)
              .NotEmpty()
              .WithMessage("O DDD é obrigatório.")
              .MaximumLength(3)
              .WithMessage("O DDD não pode exceder 3 caracteres.");
        }
    }
}
