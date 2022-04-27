using DemoCustomerAdaptApi.Domain.Interfaces;
using DemoCustomerAdaptApi.Domain.Logic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCustomerAdaptApi.Domain.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainLogic(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}