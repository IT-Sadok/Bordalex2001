using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class SignInManagerWrapper(SignInManager<AppUser> signInManager)
    : ISignInManagerWrapper<AppUser>
{
    public async Task<SignInResult> CheckPasswordSignInAsync(
        AppUser user, 
        string password, 
        bool lockoutOnFailure) =>
        await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
}
