namespace Roblox.Nonce;

using System;

using Caching.Shared;

/// <summary>
/// Memcached implementation of <see cref="IEphemeralNonce"/>.
/// </summary>
public class MemcachedEphemeralNonce : IEphemeralNonce
{
    private readonly ISharedCacheClient _SharedCacheClient;
    private readonly INonceHelper _NonceHelper;
    private readonly INonceSettings _Settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemcachedEphemeralNonce"/> class.
    /// </summary>
    /// <param name="sharedCacheClient">The shared cache client.</param>
    /// <param name="nonceHelper">The nonce helper.</param>
    /// <param name="settings">The <see cref="INonceSettings"/> to use for settings.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="sharedCacheClient"/> is null.
    /// - <paramref name="nonceHelper"/> is null.
    /// - <paramref name="settings"/> is null.
    /// </exception>
    public MemcachedEphemeralNonce(
        ISharedCacheClient sharedCacheClient,
        INonceHelper nonceHelper,
        INonceSettings settings
    ) {
        _SharedCacheClient = sharedCacheClient ?? throw new ArgumentNullException(nameof(sharedCacheClient));
        _NonceHelper = nonceHelper ?? throw new ArgumentNullException(nameof(nonceHelper));
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.GenerateEphemeralNonce"/>
    public string GenerateEphemeralNonce()
    {
        var nonce = _NonceHelper.GenerateNonce();

        try
        {
            var added = _SharedCacheClient.Add(nonce, string.Empty,  _Settings.EphemeralNonceExpiration);
            return added ? nonce : string.Empty;
        }
    #if DEBUG
        catch (Exception ex) { throw new ApplicationException("Nonce write failed", ex); }
    #else
        catch { return string.Empty; }
    #endif
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.TryRedeemEphemeralNonce"/>
    public bool TryRedeemEphemeralNonce(string nonce)
    {
        try
        {
            return _SharedCacheClient.Remove(nonce);
        }
    #if DEBUG
        catch (Exception ex) { throw new ApplicationException("Nonce redeem failed", ex); }
    #else
        catch { return false; }
    #endif
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.PurgeExpiredNonces"/>
    /// <remarks>No implementation for memcached as TTL already gets handled</remarks>
    public void PurgeExpiredNonces() {}
}
