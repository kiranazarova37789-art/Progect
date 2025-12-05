using Folivora.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Проект
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterRestController : ControllerBase
    {
        private CartrigDbContext _context;
        public PrinterRestController(CartrigDbContext context)
        {
            _context = context;
        }
        // GET: api/<PrinterRestController>
        [HttpGet]
        public IEnumerable<Printer> Get()
        {
            return _context.Printers;
        }

        // GET api/<PrinterRestController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var printer = _context.Printers.Find(id)!;

            if (printer == null) 
            {
                return NotFound($"Запись не найдена.");
            }
            return Ok(printer);
        }

        // POST api/<PrinterRestController>
        [HttpPost("{id}")]
        public IActionResult Post(Printer printer)
        {
            string? ip = Response.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (ip == "::1")
            {
                // Перебираем все IP-адреса устройства и выбираем первый доступный IPv4-адрес
                ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(); // По умолчанию localhost
            }

            if (_context.Printers.FirstOrDefault(u => u.ip == ip) != null)
            {
                return BadRequest();
            }
            printer.ip = ip.ToString();

            _context.Printers.Add(printer);
            _context.SaveChanges();
            return Ok(printer);
        }

        // PUT api/<PrinterRestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Printer printer)
        {
            var print = _context.Printers.FirstOrDefault(u => u.id_print == id);
            if (print != null)
            {
                print.ip = printer.ip;
                print.off_id = printer.off_id;
                print.cartridg_id = printer.cartridg_id;

                _context.Entry(print).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(print);
            }
            return NotFound($"Запись не найдена.");
        }

        // DELETE api/<PrinterRestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var printer = _context.Printers.Find(id);
            if (printer != null)
            {
                _context.Printers.Remove(printer);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound($"Запись не найдена.");
        }
    }
}
