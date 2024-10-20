using Discount.Grpc;

namespace Basket.API.Baskets.CreateBasket;

public record CreateBasketCommand(ShoppingCart Cart) : ICommand<CreateBasketResult>;

public record CreateBasketResult(string Username);

public class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        RuleFor(b => b.Cart).NotNull().WithMessage("Cart can't be null!");
        RuleFor(b => b.Cart.Username).NotNull().NotEmpty().WithMessage("Username is required");
    }
}

public class CreateBasketCommandHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        var basket = await repository.CreateBasket(command.Cart, cancellationToken);

        return new CreateBasketResult(basket.Username);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken : cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}

