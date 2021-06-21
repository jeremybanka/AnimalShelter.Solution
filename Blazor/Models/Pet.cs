using System;

namespace WebApi.Models
{
  public class Pet
  {
    public Pet(string name)
    {
      Id = Guid.NewGuid().ToString();
      Name = name;
    }
    public string Id { get; set; }
    public string Name { get; set; }
    public string ImgUrl { get; set; }
  }
}