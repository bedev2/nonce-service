namespace Roblox.Nonce.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Web.Framework.Services.Http;

using Models;

/// <summary>
/// Default controller.
/// </summary>
[Route("")]
[ApiController]
#if DEBUG
[AllowAnonymous]
#endif
public class DefaultController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly INonceOperations _NonceOperations;

    /// <summary>
    /// Construct a new instance of <see cref="DefaultController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="INonceOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public DefaultController(IOperationExecutor operationExecutor, INonceOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _NonceOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Generates a ephemeral nonce.
    /// </summary>
    /// <response code="400">Cannot generate nonce.</response>
    /// <response code="500">An error occurred.</response>
    /// <response code="503">Service unavailable.</response>
    [HttpPost]
    [Route($"/v1/{nameof(GenerateEphemeralNonce)}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(503)]
    public IActionResult GenerateEphemeralNonce()
        => _OperationExecutor.Execute(_NonceOperations.GenerateEphemeralNonce);

    /// <summary>
    /// Redeems a ephemeral nonce.
    /// </summary>
    /// <response code="400">Cannot redeem nonce.</response>
    /// <response code="500">An error occurred.</response>
    /// <response code="503">Service unavailable.</response>
    [HttpPost]
    [Route($"/v1/{nameof(TryRedeemEphemeralNonce)}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(503)]
    public IActionResult TryRedeemEphemeralNonce(TryRedeemEphemeralNonceRequest request)
        => _OperationExecutor.Execute(_NonceOperations.TryRedeemEphemeralNonce, request);

    /// <summary>
    /// Purges expired mssql nonces.
    /// </summary>
    /// <response code="400">Cannot purge nonces.</response>
    /// <response code="500">An error occurred.</response>
    /// <response code="503">Service unavailable.</response>
    [HttpPost]
    [Route($"/v1/{nameof(PurgeExpiredNonces)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ProducesResponseType(503)]
    public IActionResult PurgeExpiredNonces()
        => _OperationExecutor.Execute(_NonceOperations.PurgeExpiredNonces);
}
