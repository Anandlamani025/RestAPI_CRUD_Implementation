using Microsoft.AspNetCore.Mvc;
using static RestAPI_CRUD.Models;
using static RestAPI_CRUD.EntityRepository;

namespace RestAPI_CRUD.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly EntityRepository _repository;

        public EntitiesController(EntityRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Entities
        [HttpGet]
        public IEnumerable<Entity> GetEntities([FromQuery] string? search, [FromQuery] string? gender, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string[]? countries)
        {
            var entities = _repository.GetEntities();

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                entities = entities.Where(e =>
                (e.Addresses != null && e.Addresses.Any(a => a.Country.Contains(search) || a.AddressLine.Contains(search))) ||
                (e.Names != null && e.Names.Any(n => n.FirstName.Contains(search) || n.MiddleName.Contains(search) || n.Surname.Contains(search))));
                //entities = entities.Where(e =>

                //e.Addresses.Any(a => a.Country.Contains(search) || a.AddressLine.Contains(search)) ||
                //e.Names.Any(n => n.FirstName.Contains(search) || n.MiddleName.Contains(search) || n.Surname.Contains(search)));
            }

            // Filter by gender
            if (!string.IsNullOrEmpty(gender))
            {
                entities = entities.Where(e => e.Gender == gender);
            }

            // Filter by date range
            if (startDate.HasValue)
            {
                entities = entities.Where(e => e.Dates.Any(d => d.DateValue >= startDate));
            }

            if (endDate.HasValue)
            {
                entities = entities.Where(e => e.Dates.Any(d => d.DateValue <= endDate));
            }

            // Filter by countries
            if (countries != null && countries.Length > 0)
            {
                entities = entities.Where(e => e.Addresses.Any(a => countries.Contains(a.Country)));
            }

            return entities;
        }

        // GET: api/Entities/5
        [HttpGet("{id}")]
        public ActionResult<Entity> GetEntity(string id)
        {
            var entity = _repository.GetEntityById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // POST: api/Entities
        [HttpPost]
        public ActionResult<Entity> PostEntity(Entity entity)
        {
            _repository.AddEntity(entity);
            return CreatedAtAction(nameof(GetEntity), new { id = entity.Id }, entity);
        }

        // PUT: api/Entities/5
        [HttpPut("{id}")]
        public IActionResult PutEntity(string id, Entity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            _repository.UpdateEntity(entity);
            return NoContent();
        }

        // DELETE: api/Entities/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEntity(string id)
        {
            var entity = _repository.GetEntityById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.DeleteEntity(id);
            return NoContent();
        }
    }
}