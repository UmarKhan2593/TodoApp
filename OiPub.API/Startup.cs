using Application.Constants;
using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Generic;
using Application.Interfaces.UnitOfWork;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace OiPub.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region >>> DB Context Configuration <<<
            if (Configuration.GetValue<bool>(ConfigurationConstants.DbContextConfigConst.InMemoryDatabase))
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseInMemoryDatabase(ConfigurationConstants.DbContextConfigConst.IdentityDbName));
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseInMemoryDatabase(ConfigurationConstants.DbContextConfigConst.ApplicationDbName));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString(ConfigurationConstants.DbContextConfigConst.IdentityConnection)));
                services.AddDbContext<ApplicationDbContext>(
                                                options => options.UseSqlServer(Configuration.GetConnectionString(ConfigurationConstants.DbContextConfigConst.ApplicationConnection),
                                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }


            #endregion

            #region >>> Add Repositories Manually <<<
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            #region Repositories
            // we specified type specific registration so all those implement this type will be registered
            services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));
            services.AddTransient<ITodoItemRepositoryAsync, TodoItemRepository>();
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            #endregion Repositories
            #endregion






            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OiPub.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OiPub.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
