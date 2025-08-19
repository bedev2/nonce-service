namespace Roblox.Nonce.Enums;

using System.ComponentModel;

/// <summary>
/// Represents an error that can occur when executing operations related to nonce.
/// </summary>
public enum NonceError
{
    /// <summary>
    /// Cannot generate nonce.
    /// </summary>
    [Description("Cannot generate nonce.")]
    InternalError = 0,

    /// <summary>
    /// The given ephemeral nonce is invalid.
    /// </summary>
    [Description("The given ephemeral nonce is invalid.")]
    InvalidEphemeralNonce = 1
}
