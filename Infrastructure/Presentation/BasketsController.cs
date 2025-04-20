﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpGet] // GET: /api/basket?id=dasff
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManager.basketService.GetBasketAsync(id);
            return Ok(result);
        }


        [HttpPost] // POST /api/baskets
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.basketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }


        [HttpDelete] // DELETE : /api/baskets?id
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await serviceManager.basketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
