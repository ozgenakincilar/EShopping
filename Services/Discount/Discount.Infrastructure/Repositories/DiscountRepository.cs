using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("insert into coupon(ProductName,Description,Amount) values (@ProductName,@Amount,@Description)",
                                            new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {

            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("delete from coupon where ProductName=@ProductName",
                                             new { ProductName = productName });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("select * from coupon where ProductName=@ProductName",
                                                                            new { ProductName = productName });

            if (coupon == null)
            {
                return new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No discounts available."
                };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("update coupon set  ProductName=@ProductName,Amount=@Amount,Description=@Description where Id=@Id",
                                             new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description ,Id=coupon.Id});

            if (affected == 0)
            {
                return false;
            }

            return true;
        }
    }
}
