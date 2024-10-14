﻿
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccessful);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products/UpdateProduct", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            }).WithName("UpdateProduct")
              .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Update Product")
              .WithDescription("Update Product"); ;
        }
    }
}
