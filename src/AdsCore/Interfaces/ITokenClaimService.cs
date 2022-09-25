namespace Ads.Core.Interfaces;

public interface ITokenClaimService
{
    Task<string> GetToken(string userName);
}