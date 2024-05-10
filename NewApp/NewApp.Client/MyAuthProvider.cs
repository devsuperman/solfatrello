using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class MyAuthProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
         await Task.Delay(2000);
         var anonymous = new ClaimsIdentity("Cookies");
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    }
}