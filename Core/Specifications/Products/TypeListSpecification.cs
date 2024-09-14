using Core.Entities;

namespace Core.Specifications.Products;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        AddSelectClause(x => x.Type);
        ApplyDistinctFilter();
    }
}
