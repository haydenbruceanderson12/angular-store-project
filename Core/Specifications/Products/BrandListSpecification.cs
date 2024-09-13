using Core.Entities;

namespace Core.Specifications.Products;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    // This query looks something like the below:

    // DB.Table.Select(product => product.Brand).Distinct()
    public BrandListSpecification()
    {
        AddSelectClause(x => x.Brand);
        ApplyDistinctFilter();
    }
}
