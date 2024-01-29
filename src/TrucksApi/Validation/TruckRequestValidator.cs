﻿using FluentValidation;
using Microsoft.Identity.Client;
using TrucksApi.Contracts.Requests;
using TrucksApi.Domain.TruckStatuses;

namespace TrucksApi.Validation;

public class TruckRequestValidator : AbstractValidator<TruckRequest>
{
    
    public TruckRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Matches("[a-zA-Z0-9]")
            .MaximumLength(30);
        RuleFor(x=>x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Status).SetValidator(new TruckStatusValidator());
    }    
}