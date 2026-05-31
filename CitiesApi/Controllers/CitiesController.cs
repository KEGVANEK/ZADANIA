using Microsoft.AspNetCore.Mvc;
using CitiesApi.Models;

namespace CitiesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private static List<City> cities = new()
        {
            new City { Id = 1, Name = "Warszawa", Population = 1800000 },
            new City { Id = 2, Name = "Kraków", Population = 800000 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var city = cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            cities.Add(city);
            return Ok(city);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, City updatedCity)
        {
            var city = cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
                return NotFound();

            city.Name = updatedCity.Name;
            city.Population = updatedCity.Population;

            return Ok(city);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var city = cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
                return NotFound();

            cities.Remove(city);

            return NoContent();
        }
    }
}