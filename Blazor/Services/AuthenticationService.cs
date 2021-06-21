using Blazor.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.Services
{
  public interface IAuthenticationService
  {
    User User { get; }
    Task Initialize();
    Task Register(
      string firstName,
      string lastName,
      string username,
      string password1,
      string password2
      );
    Task Login(string username, string password);
    Task Logout();
  }

  public class AuthenticationService : IAuthenticationService
  {
    private readonly IHttpService _httpService;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;

    public User User { get; private set; }

    public AuthenticationService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    )
    {
      _httpService = httpService;
      _navigationManager = navigationManager;
      _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
      User = await _localStorageService.GetItem<User>("user");
    }

    public async Task Login(string username, string password)
    {
      User = await _httpService.Post<User>(
        "/users/authenticate",
        new { username, password }
        );
      await _localStorageService.SetItem("user", User);
    }

    public async Task Register(
      string firstName,
      string lastName,
      string username,
      string password1,
      string password2
      )
    {
      User = await _httpService.Post<User>(
        "/users/register",
        new
        {
          firstName,
          lastName,
          username,
          password1,
          password2
        }
      );
      await _localStorageService.SetItem("user", User);
    }

    public async Task Logout()
    {
      User = null;
      await _localStorageService.RemoveItem("user");
      _navigationManager.NavigateTo("login");
    }
  }
}