namespace Roblox.Nonce.Service;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Caching.Shared;

using Web.Framework.Services;
using Web.Framework.Services.Http;

using NonceSettings = Roblox.Nonce.Service.Settings;

/// <summary>
/// Startup class for nonce-service.
/// </summary>
public class Startup : HttpStartupBase
{
    /// <inheritdoc cref="StartupBase.Settings"/>
    protected override IServiceSettings Settings => Nonce.Service.Settings.Singleton;

    /// <inheritdoc cref="StartupBase.ConfigureServices(IServiceCollection)"/>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressConsumesConstraintForFormFileParameters = true;
        });

        services.AddSingleton<ISharedCacheClient>(SharedCacheDataClient.GetSingleton());
        services.AddSingleton<INonceHelper, NonceHelper>();
        services.AddSingleton<INonceSettings>(NonceSettings.Singleton);
        services.AddSingleton<IEphemeralNonceFactory, EphemeralNonceFactory>();

        services.AddSingleton<INonceOperations, NonceOperations>();
    }
}
