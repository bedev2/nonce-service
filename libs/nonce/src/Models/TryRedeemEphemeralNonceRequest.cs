namespace Roblox.Nonce.Models;

using System.Runtime.Serialization;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Request to try redeem a ephemeral nonce.
/// </summary>
[DataContract]
public class TryRedeemEphemeralNonceRequest
{
    /// <summary>
    /// The Ephemeral Nonce to Redeem.
    /// </summary>
    [FromQuery(Name = "nonce")]
    public string Nonce { get; set; }
}
