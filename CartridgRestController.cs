using Folivora.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using NuGet.Protocol;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Проект
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartridgRestController : ControllerBase

    {
        private CartrigDbContext _context;
        public CartridgRestController(CartrigDbContext context)
        {
            _context = context;
        }
        // GET: api/<CartridgRestController>
        [HttpGet]
        public IEnumerable<Cartridg> Get()
        {
            return _context.Cartridgs;
        }

        // GET api/<CartridgRestController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cartridg = _context.Cartridgs.Find(id)!;

            if (cartridg == null) 
            { 
                return NotFound($"Запись не найдена.");
            }
            return Ok(cartridg);
        }

        // POST api/<CartridgRestController>
        [HttpPost("{id}")]
        public IActionResult Post(Cartridg cartridg)
        {
            if (_context.Cartridgs.FirstOrDefault(u => u.id_cr == cartridg.id_cr) == null)
            {
                _context.Cartridgs.Add(cartridg);
                _context.SaveChanges();
                return Ok(cartridg);
            }
            return BadRequest();
        }

        // PUT api/<CartridgRestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Cartridg cartridg)
        {
            var cr = _context.Cartridgs.FirstOrDefault(u => u.id_cr == id);
            if (cr != null)
            {
                cr.status_cr = cartridg.status_cr;
                cr.location_cr = cartridg.location_cr;
               
                _context.Entry(cr).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(cr);
            }
            return NotFound($"Запись не найдена.");
        }

        // DELETE api/<CartridgRestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartridg = _context.Cartridgs.Find(id);
            if (cartridg != null)
            {
                _context.Cartridgs.Remove(cartridg);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound($"Запись не найдена.");
        }
    }
}
