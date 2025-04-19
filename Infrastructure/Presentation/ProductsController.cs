using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

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
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationsParameters SpecParams )
        {
            var result = await serviceManager.productService.GetAllProductAsync(SpecParams);
            if(result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/products/1
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.productService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")] // GET: /api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.productService.GetAllBrandsAync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")] // GET: /api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.productService.GetAllTypesAync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
