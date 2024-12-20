﻿using FluentValidation;
using System.Resources;
using TechChallange.Api.Controllers.Contact.Dto;

namespace TechChallange.Api.Validators.Contact
{
    public class ContactCreateValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateValidator()
        {
            RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Email inválido.");

            RuleFor(c => c.Name)
             .NotEmpty()
             .WithMessage("O nome é obrigatório.")
             .MaximumLength(50)
             .WithMessage("O nome não pode exceder 50 caracteres.");

            RuleFor(c => c.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Número de telefone inválido.")
            .When(c => !string.IsNullOrEmpty(c.Phone));

            RuleFor(c => c.RegionId)
             .NotEmpty()
             .WithMessage("O ID da região é obrigatório.");


        }
    }
}
