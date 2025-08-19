namespace Roblox.Nonce.Service;

using EventLog;
using Configuration;

using Web.Framework.Services;

using static SettingsProvidersDefaults;

internal class Settings : BaseSettingsProvider<Settings>, IServiceSettings, INonceSettings
{
    /// <inheritdoc cref="IVaultProvider.Path"/>
    protected override string ChildPath => NonceSettingsPath;

    /// <inheritdoc cref="IServiceSettings.ApiKey"/>
    public string ApiKey => GetOrDefault(nameof(ApiKey), string.Empty);

    /// <inheritdoc cref="IServiceSettings.LogLevel"/>
    public LogLevel LogLevel => GetOrDefault(nameof(LogLevel), LogLevel.Information);

    /// <inheritdoc cref="IServiceSettings.VerboseErrorsEnabled"/>
    public bool VerboseErrorsEnabled => GetOrDefault(nameof(VerboseErrorsEnabled), false);

    /// <inheritdoc cref="INonceSettings.ReadWriteNoncesMssqlEnabled"/>
    public bool ReadWriteNoncesMssqlEnabled => GetOrDefault(nameof(ReadWriteNoncesMssqlEnabled), false);

    /// <inheritdoc cref="INonceSettings.ReadWriteNoncesMemcachedEnabled"/>
    public bool ReadWriteNoncesMemcachedEnabled => GetOrDefault(nameof(ReadWriteNoncesMemcachedEnabled), true);

    /// <inheritdoc cref="INonceSettings.EphemeralNonceExpiration"/>
    public TimeSpan EphemeralNonceExpiration => GetOrDefault(nameof(EphemeralNonceExpiration), TimeSpan.FromDays(30));
}
