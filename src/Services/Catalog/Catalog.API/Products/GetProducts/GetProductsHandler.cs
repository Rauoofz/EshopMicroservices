
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? pageSize = 10, int? pageIndex = 1)
    : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.pageIndex ?? 1, query.pageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}

