using Core.Entities;

namespace Core.Specifications.Products;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParameters parameters) 
        : base(product => 
            (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search)) &&
            (parameters.Brands.Count == 0 || parameters.Brands.Contains(product.Brand)) && 
            (parameters.Types.Count == 0 || parameters.Types.Contains(product.Type)) 
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
