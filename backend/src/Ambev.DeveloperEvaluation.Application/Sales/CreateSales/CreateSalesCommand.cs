using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;


public class CreateSalesCommand : IRequest<CreateSalesResult>
{
    public Guid Id { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; }

    public decimal TotalSaleAmount { get; set; }

    public string Branch { get; set; }

    public bool IsCancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }

    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSalesValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}