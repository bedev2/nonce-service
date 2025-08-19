namespace Roblox.Nonce;

using System;

using EventLog;
using Operations;

using Enums;
using Models;

/// <summary>
/// Operation for redeeming a <see cref="IEphemeralNonce"/>.
/// </summary>
public class TryRedeemEphemeralNonceOperation : IResultOperation<TryRedeemEphemeralNonceRequest, bool>
{
    private readonly ILogger _Logger;
    private readonly IEphemeralNonceFactory _EphemeralNonceFactory;

    /// <summary>
    /// Constructs a new instance of <see cref="TryRedeemEphemeralNonceOperation"/>.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="ephemeralNonceFactory">The <see cref="IEphemeralNonceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="ephemeralNonceFactory"/> cannot be null.
    /// </exception>
    public TryRedeemEphemeralNonceOperation(
        ILogger logger,
        IEphemeralNonceFactory ephemeralNonceFactory
    ) {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _EphemeralNonceFactory = ephemeralNonceFactory ?? throw new ArgumentNullException(nameof(ephemeralNonceFactory));
    }

    /// <inheritdoc cref="IResultOperation{TOutput}"/>
    public (bool Output, OperationError Error) Execute(TryRedeemEphemeralNonceRequest request)
    {
        if (string.IsNullOrEmpty(request.Nonce)) return (false, new OperationError(NonceError.InvalidEphemeralNonce));
        return (_EphemeralNonceFactory.GetOrCreate().TryRedeemEphemeralNonce(request.Nonce), null);
    }
}
