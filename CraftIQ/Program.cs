
using CraftIQ.Models;
using CraftIQ.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRepository<Category>, CategoryRepo>();
            builder.Services.AddScoped<IRepository<Products>, ProductRepo>();
            builder.Services.AddScoped<IRepository<Order>, OrderRepo>();
            builder.Services.AddScoped<IRepository<OrderDetails>, OrderDetailsRepo>();
            builder.Services.AddScoped<IRepository<Inventory>, InventoryRepo>();
            builder.Services.AddScoped<IRepository<Transactions>, TransactionRepo>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddDbContext<AppDbContext>(e =>
            e.UseSqlServer(builder.Configuration.GetConnectionString("CS")));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
