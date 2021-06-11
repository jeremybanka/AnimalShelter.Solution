//using System.Text.Json.Serialization;
using System;

namespace WebApi.Entities
{
  public class User
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public byte[] Salt { get; set; }
    public string Hash { get; set; }
    public User(
      string firstName,
      string lastName,
      string username,
      byte[] salt,
      string hash)
    {
      Id = Guid.NewGuid().ToString();
      FirstName = firstName;
      LastName = lastName;
      Username = username;
      Salt = salt;
      Hash = hash;
    }
  }
}