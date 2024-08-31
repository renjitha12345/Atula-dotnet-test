using System.Threading.Tasks;
using AtulaHomeFurniture.Models;
using Microsoft.AspNetCore.Identity;
public class AccountViewModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginViewModel LoginViewModel { get; set; }

    public AccountViewModel(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> LoginAsync()
    {
        var result = await _signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, false, lockoutOnFailure: true);
        return result.Succeeded;
    }
}
