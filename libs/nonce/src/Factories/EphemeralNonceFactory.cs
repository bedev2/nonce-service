namespace Roblox.Nonce;

using System;

using Caching.Shared;

/// <inheritdoc cref="IEphemeralNonceFactory"/>
public class EphemeralNonceFactory : IEphemeralNonceFactory
{
    private readonly ISharedCacheClient _SharedCacheClient;
    private readonly INonceHelper _NonceHelper;
    private readonly INonceSettings _Settings;

    /// <summary>
    /// Construct a new instance of <see cref="EphemeralNonceFactory"/>
    /// </summary>
    /// <param name="sharedCacheClient">The <see cref="ISharedCacheClient"/></param>
    /// <param name="nonceHelper">The <see cref="INonceHelper"/></param>
    /// <param name="settings">The <see cref="INonceSettings"/> to use for settings.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="sharedCacheClient"/> cannot be null.
    /// - <paramref name="nonceHelper"/> cannot be null.
    /// - <paramref name="settings"/> is null.
    /// </exception>
    public EphemeralNonceFactory(
        ISharedCacheClient sharedCacheClient,
        INonceHelper nonceHelper,
        INonceSettings settings
    ) {
        _SharedCacheClient = sharedCacheClient ?? throw new ArgumentNullException(nameof(sharedCacheClient));
        _NonceHelper = nonceHelper ?? throw new ArgumentNullException(nameof(nonceHelper));
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    /// <inheritdoc cref="IEphemeralNonceFactory.GetOrCreate"/>
    public IEphemeralNonce GetOrCreate()
    {
        return _Settings switch
        {
            { ReadWriteNoncesMemcachedEnabled: true } => new MemcachedEphemeralNonce(_SharedCacheClient, _NonceHelper, _Settings),
            { ReadWriteNoncesMssqlEnabled: true }  => new MssqlEphemeralNonce(_NonceHelper, _Settings),
            _ => throw new NotImplementedException() // TODO: should we throw something else instead of not implemented here?
        };
    }
}
