﻿using Discount.Api.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using MediatR;
using System.Reflection;

namespace Discount.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateDiscountCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<IDiscountRepository, IDiscountRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DiscountService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRRPC endpoints must be made through a gRPC client");
                });
            });
        }
    }
}
