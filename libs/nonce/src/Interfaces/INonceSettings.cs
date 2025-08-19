using System;

namespace Roblox.Nonce;

/// <summary>
/// Settings used by the Nonce component.
/// </summary>
public interface INonceSettings
{
    /// <summary>
    /// Whether to read and write nonces to MSSQL.
    /// </summary>
    bool ReadWriteNoncesMssqlEnabled { get; }

    /// <summary>
    /// Whether to read and write nonces to Memcached.
    /// </summary>
    bool ReadWriteNoncesMemcachedEnabled { get; }

    /// <summary>
    /// TTL for ephemeral nonces.
    /// </summary>
    TimeSpan EphemeralNonceExpiration { get; }
}
