using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
  public class RegisterRequest
  {
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string Password1 { get; set; }
    [Required] public string Password2 { get; set; }
  }
}