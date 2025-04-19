using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductAsync(ProductSpecificationsParameters specParams);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAync();
        Task<IEnumerable<TypeDto>> GetAllTypesAync();
    }
}
