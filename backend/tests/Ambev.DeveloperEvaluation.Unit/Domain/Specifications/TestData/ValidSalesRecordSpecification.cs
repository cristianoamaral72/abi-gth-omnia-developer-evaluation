namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq;

public class ValidSalesRecordSpecification
{
    public bool IsSatisfiedBy(SalesRecord salesRecord)
    {
        if (salesRecord == null) return false;

        return !salesRecord.IsCancelled &&
               salesRecord.TotalSaleAmount > 0 &&
               salesRecord.SaleItems != null &&
               salesRecord.SaleItems.Any();
    }
}
