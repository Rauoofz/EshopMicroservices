namespace Catalog.API.Products.GetProducts;

public record GetProductRequest(int? pageSize = 10 , int? pageIndex = 1);
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequest requset, ISender sender) =>
        {
            var query = requset.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);

            var productsResponse = result.Adapt<GetProductsResponse>();

            return Results.Ok(productsResponse);

        }).WithName("GetProducts")
          .Produces<GetProductsResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Products")
          .WithDescription("Get Products");
    }
}

