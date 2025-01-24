using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for sales creation.
/// </summary>
public class CreateSalesRequestValidator : AbstractValidator<CreateSalesRequest>
{
   
    public CreateSalesRequestValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("The ID cannot be empty.");

        RuleFor(sale => sale.SaleDate)
            .NotEmpty().WithMessage("The sale date is required.")
            .Must(BeAValidDate).WithMessage("The sale date must be a valid date.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("The customer name is required.")
            .Length(3, 255).WithMessage("The customer name must be between 3 and 255 characters.");

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("The total sale amount must be greater than 0.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("The branch is required.")
            .Length(3, 255).WithMessage("The branch must be between 3 and 255 characters.");

        RuleFor(sale => sale.IsCancelled)
            .NotNull().WithMessage("The cancellation status must be specified.");
    }

    private bool BeAValidDate(DateTime arg)
    {
        return arg != default(DateTime) && arg <= DateTime.UtcNow;
    }

}