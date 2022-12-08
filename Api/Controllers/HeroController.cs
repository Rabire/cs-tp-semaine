using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private static List<Hero> heroes = new List<Hero>
        {

                new Hero {Id = 1, Name = "Iron Man", FirstName="John", LastName = "Doe", Place="Somewhere"},
                new Hero {Id = 2, Name = "Dr Manhattan", FirstName="John", LastName = "Doe", Place="Somewhere"},
                new Hero {Id = 3, Name = "Batman", FirstName="John", LastName = "Doe", Place="Somewhere"},
                new Hero {Id = 4, Name = "The Flash", FirstName="John", LastName = "Doe", Place="Somewhere"},
                new Hero {Id = 5, Name = "Capaint Cavern", FirstName="John", LastName = "Doe", Place="Somewhere"},

        };
        private readonly ApplicationDbContext dbContext;

        public HeroController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Heroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Hero>>> CreateHero([FromBody] Hero hero)
        {
            heroes.Add(hero);

            return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<Hero>>> UpdateHero(Hero request)
        {
            var hero = heroes.Find(hero => hero.Id == request.Id);
            if (hero == null)
            {
                return BadRequest(request);
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            return heroes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> Get(int id)
        {
            var hero = heroes.Find(x => x.Id == id);
            if (hero == null)
            {
                return NotFound("Hero not Found");
            }
            return Ok(hero);
        }
    }
}
