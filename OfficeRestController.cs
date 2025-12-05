using Folivora.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Проект
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeRestController : ControllerBase

    {
        private CartrigDbContext _context;
        public OfficeRestController(CartrigDbContext context)
        {
            _context = context;
        }

        // GET: api/<OfficeRestController>
        [HttpGet]
        public IEnumerable<Office> Get()
        {
            return _context.Offices;
        }

        // GET api/<OfficeRestController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {  
            var office = _context.Offices.Find(id)!;
            if (office == null)
            {
                return NotFound($"Запись не найдена.");
            }
            return Ok(office);
        }

        // POST api/<OfficeRestController>
        [HttpPost("{id}")]
        public IActionResult Post(Office office)
        {
            if (_context.Offices.FirstOrDefault(u => u.id_off == office.id_off) == null)
            {
                _context.Offices.Add(office);
                _context.SaveChanges();
                return Ok(office);
            }
            return BadRequest();
        }

        // PUT api/<OfficeRestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Office office)
        {
            //_context.Entry(office).State = EntityState.Modified;
            var off = _context.Offices.FirstOrDefault(u => u.id_off == id);
            if (off != null)
            {
                off.parent = office.parent;
                off.number_off = office.number_off;
                _context.Entry(off).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(off);
            }
            return NotFound($"Запись не найдена.");
        }


        // DELETE api/<OfficeRestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var office = _context.Offices.Find(id);
            if (office != null)
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound($"Запись не найдена.");
        }

    }
}
