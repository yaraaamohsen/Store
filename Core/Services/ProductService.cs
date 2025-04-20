using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductDto>> GetAllProductAsync(ProductSpecificationsParameters specParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications(specParams);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            
            var specCount = new ProductWithCountSpecification(specParams);
            
            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);
            
            var result = mapper.Map<IEnumerable<ProductDto>>(products);

            return new PaginationResponse<ProductDto>(specParams.PageIndex, specParams.PageSize, count, result);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications(id);
            var product = await unitOfWork.GetRepository<Product,int>().GetByIdAsync(spec);
            if (product is null ) throw new ProductNotFoundException(id);
            var result = mapper.Map<ProductDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            if(brands is null) return null;
            var result = mapper.Map<IEnumerable<BrandDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            if(types is null) return null;
            var result = mapper.Map<IEnumerable<TypeDto>>(types);
            return result;
        }
    }
}
