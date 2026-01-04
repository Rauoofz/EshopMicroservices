namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid id)
    : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(o => o.id).NotEmpty().NotNull().WithMessage("OrderId is required");
    }
}