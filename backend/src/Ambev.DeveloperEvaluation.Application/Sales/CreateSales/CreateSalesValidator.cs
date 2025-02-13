using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesValidator()
    {
        // Validate ID
        RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("The ID cannot be empty.");

        // Validate SaleDate
        RuleFor(sale => sale.SaleDate)
            .NotEmpty().WithMessage("The sale date is required.")
            .Must(BeAValidDate).WithMessage("The sale date must be a valid date and cannot be in the future.");

        // Validate CustomerName
        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("The customer name is required.")
            .Length(3, 255).WithMessage("The customer name must be between 3 and 255 characters.");

        // Validate TotalSaleAmount
        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("The total sale amount must be greater than 0.");

        // Validate Branch
        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("The branch is required.")
            .Length(3, 255).WithMessage("The branch must be between 3 and 255 characters.");

        // Validate SaleItems
        RuleFor(sale => sale.SaleItems)
            .NotEmpty().WithMessage("The sale must contain at least one item.")
            .Must(HaveValidSaleItems).WithMessage("One or more sale items are invalid.");

        RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer name is required.");
        RuleFor(x => x.TotalSaleAmount).GreaterThan(0).WithMessage("Total sale amount must be greater than zero.");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale date is required.");

        // Validate each SaleItem
        RuleForEach(sale => sale.SaleItems).ChildRules(items =>
        {
            items.RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("The quantity cannot exceed 20.");

            items.RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("The unit price must be greater than 0.");

            items.RuleFor(item => item.ProductName)
                .NotEmpty().WithMessage("The product name is required.")
                .Length(3, 255).WithMessage("The product name must be between 3 and 255 characters.");
        });
    }

    private bool BeAValidDate(DateTime date)
    {
        return date != default(DateTime) && date <= DateTime.UtcNow;
    }

    private bool HaveValidSaleItems(ICollection<SaleItem> saleItems)
    {
        return saleItems.All(item => item.Quantity > 0 && item.Quantity <= 20 && item.UnitPrice > 0);
    }
}
