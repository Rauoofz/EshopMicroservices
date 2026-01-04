
namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderResult(Guid Id);
public record CreateOrderCommand(OrderDto Order)
    :ICommand<CreateOrderResult>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().NotNull().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().NotNull().WithMessage("OrderItems should not be empty");
    }
}