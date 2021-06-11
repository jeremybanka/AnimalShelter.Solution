using System;

namespace WebApi.Models
{
  public class Dog : Pet
  {
    public Dog(string name) : base(name)
    {
      Random eny = new();
      int x = eny.Next(100, 1001);
      int y = eny.Next(100, 1001);
      ImgUrl = $"http://place-puppy.com/{x}x{y}";
    }
  }
}