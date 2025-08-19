namespace Roblox.Nonce.Service;

/// <summary>
/// Default details for the settings providers.
/// </summary>
internal static class SettingsProvidersDefaults
{
    /// <summary>
    /// The path prefix for the web platform.
    /// </summary>
    public const string ProviderPathPrefix = "services";

    /// <summary>
    /// The path to the key management settings.
    /// </summary>
    public const string NonceSettingsPath = $"{ProviderPathPrefix}/nonce-service";
}
