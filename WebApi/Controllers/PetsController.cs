using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiVersion("1.0")]
  //[Route("api/{v:apiVersion}/")]
  [Route("api/")]
  [ApiController]
  public class PetsController : ControllerBase
  {
    private readonly WebApiContext _db;

    public PetsController(WebApiContext db)
    {
      _db = db;
    }

    private bool PetExists(string id)
    => _db.Cats.Any(a => a.Id == id) ||
       _db.Dogs.Any(a => a.Id == id);

    private async Task<Pet> PetWithId(string id)
    => await CatWithId(id) ?? await DogWithId(id);
    private async Task<Pet> CatWithId(string id) => await _db.Cats.FindAsync(id);
    private async Task<Pet> DogWithId(string id) => await _db.Dogs.FindAsync(id);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAllPets(string dogOrCat)
    => dogOrCat switch
    {
      "cat" => await _db.Cats.ToListAsync(),
      "dog" => await _db.Dogs.ToListAsync(),
      _ => NotFound()
    };
    [HttpGet("cats")]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAllCats(string id)
    => await GetAllPets("cat");

    [HttpGet("dogs")]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAllDogs(string id)
    => await GetAllPets("dog");

    [HttpGet("pets/{id}")]
    public async Task<ActionResult<Pet>> GetPet(string id, string dogOrCat)
    => dogOrCat switch
    {
      "cat" => await CatWithId(id),
      "dog" => await DogWithId(id),
      _ => NotFound()
    };
    [HttpGet("cats/{id}")]
    public async Task<ActionResult<Pet>> GetCat(string id)
    => await GetPet(id, "cat");

    [HttpGet("dogs/{id}")]
    public async Task<ActionResult<Pet>> GetDog(string id)
    => await GetPet(id, "dog");

    [HttpPost("cats/")]
    public async Task<ActionResult<Cat>> PostCat(PetData data)
    {
      Cat c = new(data.Name);
      _db.Cats.Add(c);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(CatWithId), new { id = c.Id }, c);
    }
    [HttpPost("dogs/")]
    public async Task<ActionResult<Dog>> PostDog(PetData data)
    {
      Dog d = new(data.Name);
      _db.Dogs.Add(d);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(DogWithId), new { id = d.Id }, d);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, PetData pd)
    {
      Pet p = await PetWithId(id);
      if (p == null) return BadRequest();
      p.Name = pd.Name;
      _db.Entry(p).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PetExists(id)) return NotFound();
        else throw;
      }
      return NoContent();
    }

    [HttpDelete("cats/{id}")]
    public async Task<IActionResult> DeleteCat(string id)
    {
      if (await CatWithId(id) is not Cat c) return NotFound();
      _db.Cats.Remove(c);
      await _db.SaveChangesAsync();
      return NoContent();
    }
    [HttpDelete("dogs/{id}")]
    public async Task<IActionResult> DeleteDog(string id)
    {
      if (await DogWithId(id) is not Dog d) return NotFound();
      _db.Dogs.Remove(d);
      await _db.SaveChangesAsync();
      return NoContent();
    }
  }
}
