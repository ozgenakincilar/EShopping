﻿using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with the product name = {request.ProductName} not found."));
            }

            //TODO: Excercise Follow product mapper kinda example

            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
            };

            return couponModel;
        }
    }
}
