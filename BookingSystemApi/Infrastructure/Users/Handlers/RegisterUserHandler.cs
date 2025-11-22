using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.RegisterUser;

public class RegisterUserHandler(
    IUserManagerWrapper<AppUser> userManager,
    IRoleManagerWrapper<IdentityRole> roleManager)