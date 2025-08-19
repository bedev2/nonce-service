namespace Roblox.Nonce;

using System;

using EventLog;
using Operations;

/// <summary>
/// Operation for redeeming a <see cref="IEphemeralNonce"/>.
/// </summary>
public class PurgeExpiredNoncesOperation : IOperation
{
    private readonly ILogger _Logger;
    private readonly IEphemeralNonceFactory _EphemeralNonceFactory;

    /// <summary>
    /// Constructs a new instance of <see cref="PurgeExpiredNoncesOperation"/>.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="ephemeralNonceFactory">The <see cref="IEphemeralNonceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="ephemeralNonceFactory"/> cannot be null.
    /// </exception>
    public PurgeExpiredNoncesOperation(
        ILogger logger,
        IEphemeralNonceFactory ephemeralNonceFactory
    ) {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _EphemeralNonceFactory = ephemeralNonceFactory ?? throw new ArgumentNullException(nameof(ephemeralNonceFactory));
    }

    /// <inheritdoc cref="IResultOperation{TOutput}"/>
    public OperationError Execute()
    {
        _EphemeralNonceFactory.GetOrCreate().PurgeExpiredNonces();
        return null;
    }
}
