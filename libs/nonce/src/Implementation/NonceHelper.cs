namespace Roblox.Nonce;

using System;
using System.Security.Cryptography;

/// <inheritdoc cref="INonceHelper"/>
public class NonceHelper : INonceHelper
{
    /// <inheritdoc cref="INonceHelper.GenerateNonce"/>
    public string GenerateNonce()
    {
        var buffer = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(buffer);

        return Convert.ToBase64String(buffer)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
