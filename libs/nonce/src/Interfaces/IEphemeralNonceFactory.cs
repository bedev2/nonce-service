namespace Roblox.Nonce;

/// <summary>
/// Factory for getting or creating <see cref="IEphemeralNonce"/>.
/// </summary>
public interface IEphemeralNonceFactory
{
    /// <summary>
    /// Get or create a <see cref="IEphemeralNonce"/>.
    /// </summary>
    /// <returns>The <see cref="IEphemeralNonce"/>.</returns>
    IEphemeralNonce GetOrCreate();
}
