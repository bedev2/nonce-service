namespace Roblox.Nonce;

/// <summary>
/// Operations for nonce.
/// </summary>
public interface INonceOperations
{
    /// <summary>
    /// Gets the <see cref="GenerateEphemeralNonceOperation"/>.
    /// </summary>
    GenerateEphemeralNonceOperation GenerateEphemeralNonce { get; }

    /// <summary>
    /// Gets the <see cref="TryRedeemEphemeralNonceOperation"/>.
    /// </summary>
    TryRedeemEphemeralNonceOperation TryRedeemEphemeralNonce { get; }

    /// <summary>
    /// Gets the <see cref="PurgeExpiredNoncesOperation"/>.
    /// </summary>
    PurgeExpiredNoncesOperation PurgeExpiredNonces { get; }
}
