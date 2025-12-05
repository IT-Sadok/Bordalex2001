using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Interfaces;

public interface ISignInManagerWrapper<TUser> 
    where TUser : class
{
    Task<SignInResult> CheckPasswordSignInAsync(
        TUser user, 
        string password, 
        bool lockoutOnFailure);
}
