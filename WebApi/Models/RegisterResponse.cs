using WebApi.Entities;

namespace WebApi.Models
{
  public class RegisterResponse
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public byte[] Salt { get; set; }
    public string Hash { get; set; }

    public RegisterResponse(User user)
    {
      Id = user.Id;
      FirstName = user.FirstName;
      LastName = user.LastName;
      Username = user.Username;
    }
  }
}