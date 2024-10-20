using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext,ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (coupon is null)
            return new CouponModel { ProductName = "No Discount", Description = "No Discount", Amount = 0 };

        logger.LogInformation("Discount retrieved for productName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument !"));

        await dbContext.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount Added Successfully for ProductName : {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument !"));

        dbContext.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount updated Successfully for ProductName : {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, @$"Discount with ProductName= {request.ProductName} not found!"));

        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount removed for productName: {productName}", coupon.ProductName);

        return new DeleteDiscountResponse { IsSuccess = true};
    }
}
