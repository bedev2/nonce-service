namespace Roblox.Nonce;

/// <summary>
/// Interface representing an ephemeral nonce.
/// </summary>
public interface IEphemeralNonce
{
    /// <summary>
    /// Generates a ephemeral nonce.
    /// </summary>
    /// <returns>The nonce.</returns>
    string GenerateEphemeralNonce();

    /// <summary>
    /// Tries to redeem a ephemeral nonce.
    /// </summary>
    /// <param name="nonce">The nonce.</param>
    /// <returns><c>true</c> if redeemed</returns>
    bool TryRedeemEphemeralNonce(string nonce);

    /// <summary>
    /// Purges expired nonces.
    /// </summary>
    void PurgeExpiredNonces();
}
