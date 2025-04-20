﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IBasketRepository basketRepository) : IServiceManager
    {
        public IProductService productService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService basketService { get; } = new BasketService(basketRepository, mapper);
    }
}
