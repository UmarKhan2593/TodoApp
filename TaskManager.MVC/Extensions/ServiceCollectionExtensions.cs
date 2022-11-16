
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Reflection;
using TaskManager.Infrastructure.Shared.Constants.IdentityConst;
using TaskManager.Infrastructure.Shared.Interfaces.Shared;
using TaskManager.Infrastructure.Shared.Services;
using TaskManager.Presentation.Web.Infrastructure.Authentication;
using TaskManager.Presentation.Web.Infrastructure.Constants.Application;
using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers;
using TaskManager.Presentation.Web.Services;

namespace TaskManager.Presentation.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {

        #region >>> Add Infrastructure <<<
        public static void AddWebInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                //TODO:- Session - 20 minutes later from last access your session will be removed.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddRouting();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddManagers();
            services.AddSharedInfrastructure(configuration);
            services.AddWebServices(configuration);


            // Check Do we still need that 

            services.AddTransient<AuthenticationHeaderHandler>();

            services.AddHttpContextAccessor();
            services.AddHttpClient("", client =>
            {
                client.BaseAddress = new Uri(ApplicationConstants.Base_Url);
                client.Timeout = TimeSpan.FromMinutes(10);
                client.DefaultRequestHeaders.AcceptLanguage.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
            })
            .AddHttpMessageHandler<AuthenticationHeaderHandler>();

        }
        #endregion



        #region >>> Register Permission Claims <<<
        // Not required
        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(CustomClaimTypes.Permission, propertyValue.ToString()));
                }
            }
        }
        #endregion

        #region >>> Add Managers <<<

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);
            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }
        #endregion

        #region >>> Add Infrastructure <<<
        /// <summary>
        /// Add Mail setting and service, IDateTimeService, AuthenticatedUserService
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, SystemDateTimeService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
        }
        #endregion

        #region >>> Web Services <<<
        // TODO:- Enhancement - Register services here WEB .
        public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {

        }
        #endregion


    }
}

