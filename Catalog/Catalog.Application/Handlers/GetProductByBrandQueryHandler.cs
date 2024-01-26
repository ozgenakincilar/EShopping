using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandQueryHandler:IRequestHandler<GetProductByBrandQuery,IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductByBrand(request.BrandName);
            var productListResponse=ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productListResponse;
        }
    }
}
