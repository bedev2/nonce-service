namespace Roblox.Nonce;

using System;

using EventLog;

/// <summary>
/// Implementation of <see cref="INonceOperations"/>.
/// </summary>
/// <seealso cref="INonceOperations"/>
public class NonceOperations : INonceOperations
{
    /// <inheritdoc cref="INonceOperations.GenerateEphemeralNonce"/>
    public GenerateEphemeralNonceOperation GenerateEphemeralNonce { get; }

    /// <inheritdoc cref="INonceOperations.TryRedeemEphemeralNonce"/>
    public TryRedeemEphemeralNonceOperation TryRedeemEphemeralNonce { get; }

    /// <inheritdoc cref="INonceOperations.PurgeExpiredNonces"/>
    public PurgeExpiredNoncesOperation PurgeExpiredNonces { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="NonceOperations"/>.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use.</param>
    /// <param name="ephemeralNonceFactory">The <see cref="IEphemeralNonceFactory"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ephemeralNonceFactory"/> is null.
    /// </exception>
    public NonceOperations(ILogger logger, IEphemeralNonceFactory ephemeralNonceFactory)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(ephemeralNonceFactory, nameof(ephemeralNonceFactory));

        GenerateEphemeralNonce = new(logger, ephemeralNonceFactory);
        TryRedeemEphemeralNonce = new(logger, ephemeralNonceFactory);
        PurgeExpiredNonces =  new(logger, ephemeralNonceFactory);
    }
}
