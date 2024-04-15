//STEP 02 - Generate the C# Models:
//Scaffold-DbContext "Server=.\sqlexpress;Database=ToDo;Trusted_Connection=true;Encrypt=false;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

//STEP 03 - Add using for EntityFramework
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
                });

            //STEP 05 - Scaffold out a new API Controller using Entity Framework
            //Right-Click the Controllers folder -> Add -> Controller....
            //Choose an API Controller with actions using Entity Framework
            //Model: Category
            //DataContext: ResourcesContext
            //After scaffolding, test inthe browser with Swagger
            //Next Step: Create a controller for Resources using the same method,
            //Then see ResourcesController for Step 6

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}
