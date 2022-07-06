using IntegrifyAssignment.Models;
using IntegrifyAssignment.Interface;
using IntegrifyAssignment.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


namespace IntegrifyAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<TodoContext>();
        builder.Services.AddDbContext<UserContext>();
        builder.Services.AddTransient<IUsers, UserRepository>();
        builder.Services.AddTransient<ITodos, TodoRepository>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();


        }
    }
}

