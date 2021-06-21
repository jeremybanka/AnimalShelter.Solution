using Blazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Services
{
  public interface IPetService
  {
    Task<IEnumerable<User>> GetAll();
  }

  public class PetService
  {
    private readonly IHttpService _httpService;

    public PetService(IHttpService httpService)
    {
      _httpService = httpService;
    }

    // public async Task<IEnumerable<Pet>> GetAll()
    // => await _httpService.Get<IEnumerable<Pet>>("/api/cats");

  }
}