using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CommitMaster.Sirius.App.Extensions
{
    public static class MediatorExtensions
    {
        public static void AddCustomMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
