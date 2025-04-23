namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService productService { get; }
        IBasketService basketService { get; }
        ICacheService cacheService { get; }
        IAuthService authService { get; }
    }
}
