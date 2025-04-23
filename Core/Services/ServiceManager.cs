using AutoMapper;
using Domain.Contracts;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository,
        ICacheRepository cacheRepository,
        UserManager<AppUser> userManager) : IServiceManager
    {
        public IProductService productService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService basketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService cacheService { get; } = new CacheService(cacheRepository);

        public IAuthService authService { get; } = new AuthService(userManager);
    }
}
