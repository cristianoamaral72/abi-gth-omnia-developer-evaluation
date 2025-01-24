using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Validator for GetSalesCommand
/// </summary>
public class GetSalesValidator : AbstractValidator<GetSalesCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSalesCommand
    /// </summary>
    public GetSalesValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sales ID is required");
    }
}
