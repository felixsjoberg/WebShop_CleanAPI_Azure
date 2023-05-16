using Application.Common.Interfaces.Persistence;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    public async Task<bool> ValidateCredientals(string username, string password)
    {
        // Wonder what happens if we pass in a username that doesn't exist?
        var user = await _userManager.FindByNameAsync(username);

        return await _userManager.CheckPasswordAsync(user!, password);
    }

    public async Task<ApplicationUser?> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> Register(string email, string username, string password)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = username
        };

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }
    public async Task<bool> AdminRegister(string email, string username, string password)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = username
        };

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }
}
// public async Task Login(UserLogin user)
// {
//     ValidateCredientals(user.UserName, user.Password);
//     var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
// }