
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
using Store.Owner.Extensions;
using Store.Owner.Middlewares;

namespace Store.Owner
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
