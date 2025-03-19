using EGO.Gladius.Core;

using Microsoft.Extensions.DependencyInjection;

namespace EGO.Gladius.Extensions;

public static class DIFilter
{
    public static void FilterMonadServices(this IServiceCollection collection)
    {
        foreach (var service in collection.ToList())
        {
            if (service.ImplementationType is null)
                continue;

            var extended = SP.Extend(service.ImplementationType);

            if (extended is null) continue;

            collection.Remove(service);

            collection.Add(ServiceDescriptor.Describe(
                service.ServiceType,
                extended,
                service.Lifetime));
        }
    }
}
