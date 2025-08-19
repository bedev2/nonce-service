namespace Roblox.Nonce;

using System;

using EventLog;
using Operations;

using Enums;

/// <summary>
/// Operation for generating a <see cref="IEphemeralNonce"/>.
/// </summary>
public class GenerateEphemeralNonceOperation : IResultOperation<string>
{
    private readonly ILogger _Logger;
    private readonly IEphemeralNonceFactory _EphemeralNonceFactory;

    /// <summary>
    /// Constructs a new instance of <see cref="GenerateEphemeralNonceOperation"/>.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="ephemeralNonceFactory">The <see cref="IEphemeralNonceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="ephemeralNonceFactory"/> cannot be null.
    /// </exception>
    public GenerateEphemeralNonceOperation(
        ILogger logger,
        IEphemeralNonceFactory ephemeralNonceFactory
    ) {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _EphemeralNonceFactory = ephemeralNonceFactory ?? throw new ArgumentNullException(nameof(ephemeralNonceFactory));
    }

    /// <inheritdoc cref="IResultOperation{TOutput}"/>
    public (string Output, OperationError Error) Execute()
    {
        var nonce = _EphemeralNonceFactory.GetOrCreate().GenerateEphemeralNonce();
        if (string.IsNullOrEmpty(nonce)) return (null, new OperationError(NonceError.InternalError));

        return (nonce, null);
    }
}
