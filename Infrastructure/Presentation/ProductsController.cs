using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrotModels;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // endpoint: public non-static method

        // sort: nameasc [default]
        // sort: namedesc
        // sort: priceasc 
        // sort: pricedesc 

        [HttpGet] // GET: api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters SpecParams )
        {
            var result = await serviceManager.productService.GetAllProductAsync(SpecParams);
            return Ok(result);
        }


        [HttpGet("{id}")] // GET: /api/products/1
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }


        [HttpGet("brands")] // GET: /api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAync();
            if (result is null) return BadRequest();
            return Ok(result);
        }


        [HttpGet("types")] // GET: /api/products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
