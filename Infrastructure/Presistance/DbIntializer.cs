using System.Text.Json;
using Domain.Contracts;
using Domain.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Identity;
using Presistance.Data;

namespace Presistance
{
    public class DbIntializer : IDbIntializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbIntializer(StoreDbContext context,
            StoreIdentityDbContext IdentityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _identityDbContext = IdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task IntializeAsync()
        {
            try
            {
                // Create Database If It Doesn't Exists && Apply Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                // Data Seeding

                // seeding ProductTypes From Json Files
                if (!_context.ProductType.Any())
                {
                    // 1. Read All Data From Types Json File As String
                    var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\types.json");

                    // 2. Transform String To C# Object [List Of ProductTypes] [Deserialize => from string to C# Object]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                    // 3. Add List<ProductTypes> To Database    
                    if (types != null && types.Any())
                    {
                        await _context.ProductType.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                // seeding ProductBrands From Json Files
                if (!_context.ProductBrand.Any())
                {
                    var BrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    if (brands != null && brands.Any())
                    {
                        await _context.ProductBrand.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                // seeding Product From Json Files
                if (!_context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task IntializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });
            }

            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01234567890"
                };

                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01234567890"
                };

                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
