using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTaskApp.Data;
using TestTaskApp.Helpers;

namespace TestTaskApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllers();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(opt => opt
            .WithOrigins(new[] {"http://localhost:3000", "http://localhost:8080", "http://localhost:4200" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
