namespace Roblox.Nonce;

using System;
using System.Data;
using System.Data.SqlClient;

using MssqlDatabases;

/// <summary>
/// MSSQL implementation of <see cref="IEphemeralNonce"/>.
/// </summary>
public class MssqlEphemeralNonce : IEphemeralNonce
{
    private readonly RobloxDatabase _Database = RobloxDatabase.RobloxNonces;
    private readonly INonceHelper _NonceHelper;
    private readonly INonceSettings _Settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MssqlEphemeralNonce"/> class.
    /// </summary>
    /// <param name="nonceHelper">The nonce helper.</param>
    /// <param name="settings">The <see cref="INonceSettings"/> to use for settings.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="nonceHelper"/> is null.
    /// - <paramref name="settings"/> is null.
    /// </exception>
    public MssqlEphemeralNonce(
        INonceHelper nonceHelper,
        INonceSettings settings
    ) {
        _NonceHelper = nonceHelper ?? throw new ArgumentNullException(nameof(nonceHelper));
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.GenerateEphemeralNonce"/>
    public string GenerateEphemeralNonce()
    {
        var nonce = _NonceHelper.GenerateNonce();

        try
        {
            var isInserted = new SqlParameter("@IsInserted", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            var queryParameters = new SqlParameter[]
            {
                isInserted,
                new SqlParameter("@Nonce", nonce),
                new SqlParameter("@ExpirationSeconds", _Settings.EphemeralNonceExpiration.TotalSeconds)
            };

            _Database.ExecuteNonQuery("[dbo].[EphemeralNonces_InsertEphemeralNonce]", queryParameters);

            return (bool)isInserted.Value ? nonce : string.Empty;
        }
        #if DEBUG
        catch (Exception ex) { throw new ApplicationException("Nonce write failed", ex); }
        #else
        catch { return string.Empty; }
        #endif
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.TryRedeemEphemeralNonce"/>
    public bool TryRedeemEphemeralNonce(string nonce)
    {
        try
        {
            var isRedeemed = new SqlParameter("@IsRedeemed", SqlDbType.Bit) { Direction = ParameterDirection.Output };

            var queryParameters = new SqlParameter[]
            {
                isRedeemed,
                new SqlParameter("@Nonce", nonce)
            };

            _Database.ExecuteNonQuery("[dbo].[EphemeralNonces_TryRedeem]", queryParameters);

            return (bool)isRedeemed.Value;
        }
        #if DEBUG
        catch (Exception ex) { throw new ApplicationException("Nonce redeem failed", ex); }
        #else
        catch { return false; }
        #endif
    }

    /// <inheritdoc cref="Roblox.Nonce.IEphemeralNonce.PurgeExpiredNonces"/>
    public void PurgeExpiredNonces()
    {
        try
        {
            var deletedCount = new SqlParameter("@DeletedCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var queryParameters = new SqlParameter[]
            {
                deletedCount
            };

            _Database.ExecuteNonQuery("[dbo].[EphemeralNonces_PurgeExpiredNonces]", queryParameters);
        }
        #if DEBUG
        catch (Exception ex) { throw new ApplicationException("Nonce purge failed", ex); }
        #else
        catch { }
        #endif
    }
}
