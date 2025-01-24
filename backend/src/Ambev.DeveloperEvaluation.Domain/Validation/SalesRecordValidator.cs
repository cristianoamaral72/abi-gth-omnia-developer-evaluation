using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SalesRecordValidator : AbstractValidator<SalesRecord>
{
    public SalesRecordValidator()
    {
        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("The sale date is required.")
            .Must(date => date > DateTime.MinValue).WithMessage("The sale date is invalid.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("The customer name is required.")
            .Length(3, 255).WithMessage("The customer name must be between 3 and 255 characters.");

        RuleFor(x => x.TotalSaleAmount)
            .GreaterThan(0).WithMessage("The total sale value must be greater than 0.");

        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("The branch name is required.")
            .Length(3, 255).WithMessage("The branch name must be between 3 and 255 characters.");

        RuleFor(x => x.SaleItems)
            .NotEmpty().WithMessage("The sale must contain at least one item.");
    }
}
