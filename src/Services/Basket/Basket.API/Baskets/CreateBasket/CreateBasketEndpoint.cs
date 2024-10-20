namespace Basket.API.Baskets.CreateBasket;

public record CreateBasketRequest(ShoppingCart Cart);

public record CreateBasketResponse(string Username);
public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Basket/CreateBasket", async (CreateBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateBasketResponse>();

            return Results.Ok(response);
        }).WithName("CreateBasket")
          .Produces<CreateBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Create Basket")
          .WithDescription("Create Basket");
    }
}
