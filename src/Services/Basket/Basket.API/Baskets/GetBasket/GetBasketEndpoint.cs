﻿namespace Basket.API.Baskets.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Basket/GetBasket/{Username}", async (string Username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(Username));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);

        }).WithName("GetBasket")
          .Produces<GetBasketResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Basket")
          .WithDescription("Get Basket");
    }
}

