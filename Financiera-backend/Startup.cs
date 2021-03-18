using Financiera_backend.Entity;
using Financiera_backend.Interfaces;
using Financiera_backend.ServicesDapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Financiera_backend
{
    public class Startup
    {
        readonly string Todos = "Todos";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(options =>
            {
                options.AddPolicy(Todos,
                builder =>
                {

                    builder.WithOrigins("http://localhost:8080", "https://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                 });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Financiera_backend", Version = "v1" });
            });
            services.AddDbContext<DBContexto>(options => options.UseMySql(Configuration.GetConnectionString("Connection")));
            services.AddTransient<IDbConnection>(db => new MySqlConnection(
                  Configuration.GetConnectionString("Connection")
              ));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRecordService, RecordService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financiera_backend v1"));
            }


            app.UseCors(Todos);
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
