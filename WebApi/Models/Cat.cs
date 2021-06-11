using System;

namespace WebApi.Models
{
  public class Cat : Pet
  {
    public Cat(string name) : base(name)
    {
      Random eny = new();
      int x = eny.Next(100, 1001);
      int y = eny.Next(100, 1001);
      ImgUrl = $"http://placekitten.com/{x}/{y}";
    }
  }
}