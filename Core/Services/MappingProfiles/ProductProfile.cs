﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.productBrand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.productType.Name))
                //.ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"https://localhost:7251/{ s.PictureUrl}"));
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
