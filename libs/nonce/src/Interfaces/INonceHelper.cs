namespace Roblox.Nonce;

/// <summary>
/// Helper interface for nonces.
/// </summary>
public interface INonceHelper
{
    /// <summary>
    /// Generate a <see cref="IEphemeralNonce"/>.
    /// </summary>
    /// <returns>The <see cref="IEphemeralNonce"/>.</returns>
    string GenerateNonce();
}
