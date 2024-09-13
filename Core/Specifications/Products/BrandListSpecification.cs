using Core.Entities;

namespace Core.Specifications.Products;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        // product = T (Product) & product.Brand = TResult (string)
        AddSelectClause(product => product.Brand);

        
        ApplyDistinctFilter();
    }
}
