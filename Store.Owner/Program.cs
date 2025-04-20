
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Presistance;
using Presistance.Data;
using Services;
using Services.Abstractions;
using Shared.ErrotModels;
using Store.Owner.Middlewares;
using AssemblyMapping = Services.AssemblyReference;

namespace Store.Owner
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            //builder.Services.AddScoped<IDbIntializer, DbIntializer>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            //builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);
            //builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddApplicationServices();

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                    .Select(m => new ValidationError()
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                    });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();

            // Seeding
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbIntializer.IntializeAsync();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
