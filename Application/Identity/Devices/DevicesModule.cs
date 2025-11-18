using IGCSELearningHub.Application.Identity.Devices.Interfaces;
using IGCSELearningHub.Application.Identity.Devices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Identity.Devices;

public static class DevicesModule
{
    public static IServiceCollection AddDevicesModule(this IServiceCollection services)
    {
        services.AddScoped<IDeviceService, DeviceService>();
        return services;
    }
}
