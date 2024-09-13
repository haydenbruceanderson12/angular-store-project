using Core.Entities;

namespace Core.Specifications.Products;

public class ProductSpecification : BaseSpecification<Product>
{
    // This query looks something like the below:
    
    // DB.Table.Where(product => *product.Contains(brands) && *product.Contains(types))
    //      .Skip(x)
    //      .Take(x)
    //      .OrderyBy*(product.Name)
    public ProductSpecification(ProductSpecificationParameters parameters) : base(product => 
        (!parameters.Brands.Any() || parameters.Brands.Contains(product.Brand)) && 
        (!parameters.Types.Any()  || parameters.Types.Contains(product.Type)) 
    )
    {
        ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);

        switch (parameters.Sort)
        {
            case "priceAsc":
                AddOrderByAscending(product => product.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(product => product.Price);
                break;
            default:
                AddOrderByAscending(product => product.Name);
                break;
        }
    }
}
