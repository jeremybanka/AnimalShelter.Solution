using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
  public interface IUserService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(string id);
  }

  public class UserService : IUserService
  {
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    // private readonly List<User> _users = new()
    // {
    //   new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test" }
    // };

    private readonly WebApiContext _db;

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings, WebApiContext db)
    {
      _db = db;
      _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
      var user = _db.Users.FirstOrDefault(u => u.Username == model.Username);

      // return null if user not found
      if (user == null) return null;

      // authentication successful so generate jwt token
      return Hasher.Verify(model.Password, user)
        ? new AuthenticateResponse(user, GenerateJwtToken(user))
        : null;
    }

    public void Register(RegisterRequest rr)
    {
      var existingUser = _db.Users.SingleOrDefault(u => u.Username == rr.Username);

      if (existingUser != null) return;
      if (rr.Password1 != rr.Password2) return;

      var (salt, hash) = Hasher.Make(rr.Password1);
      // authentication successful so generate jwt token
      _db.Users.Add(new User(
          rr.FirstName,
          rr.LastName,
          rr.Username,
          salt,
          hash
      ));
    }

    public IEnumerable<User> GetAll() => _db.Users.ToList();

    public User GetById(string id) => _db.Users.FirstOrDefault(u => u.Id == id);

    // helper methods

    private string GenerateJwtToken(User user)
    {
      // generate token that is valid for 7 days
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
          )
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}