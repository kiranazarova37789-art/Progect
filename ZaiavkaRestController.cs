using Folivora.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Проект
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaiavkaRestController : ControllerBase
    {
        private CartrigDbContext _context;
        private ITelegramBotClient _botClient;
        public ZaiavkaRestController(CartrigDbContext context, ITelegramBotClient botClient)
        {
            _context = context;
            _botClient = botClient;
        }
        // GET: api/<ZaiavkaRestController>
        [HttpGet]
        public IEnumerable<Zaiavka> Get()
        {
            return _context.Zaiavkas;
        }

        // GET api/<ZaiavkaRestController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var zaiavka = _context.Zaiavkas.Find(id);

            if (zaiavka == null)
            {
                return NotFound($"Запись не найдена.");
            }

            return Ok(zaiavka);
        }
        
        // GET api/<ZaiavkaRestController>/new
        [HttpGet("new")]
        public IActionResult CreateNew(int id)
        {
            return CreateNewRequest(new Zaiavka());
        }

        // POST api/<ZaiavkaRestController>
        [HttpPost("{id}")]
        public IActionResult Post(Zaiavka zaiavka)
        {
            return CreateNewRequest(zaiavka);
        }

        protected IActionResult CreateNewRequest(Zaiavka zaiavka)
        {
            //try to create request from query arguments
            string? printerId = HttpContext.Request.Query.FirstOrDefault(q => q.Key == "printer").Value;

            string? ip = "";
            if (string.IsNullOrEmpty(printerId))
            {
                ip = Response.HttpContext.Connection.RemoteIpAddress?.ToString();
                
                if (ip == "::1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(); 
                }
            }
            else
            {
                ip =_context.Printers.FirstOrDefault(u => u.id_print == int.Parse(printerId)).ip; 
            }

            if (_context.Zaiavkas.FirstOrDefault(u => u.ip_printer == ip.ToString()) != null)
            {
                return NotFound($"Такого ip было ненайдено.");
            }
            zaiavka.ip_printer = ip.ToString();

          
            Printer? print = _context.Printers.FirstOrDefault(u => u.ip == zaiavka.ip_printer);
            if (print == null)
            {
                return NotFound();
            }

            Office? offices = _context.Offices.FirstOrDefault(u => u.id_off == print.off_id);
            if (offices == null)
            {
                return NotFound();
            }
            zaiavka.num_off = offices.number_off;
            zaiavka.parent_off = offices.parent;

            Cartridg? cartridgs = _context.Cartridgs.FirstOrDefault(u => u.id_cr == print.cartridg_id);
            if (cartridgs == null)
            {
                return NotFound();
            }
            zaiavka.cartridg_model = cartridgs.model_cr;
         
            zaiavka.status_zv = "Открыта";

            _context.Zaiavkas.Add(zaiavka);
            _context.SaveChanges();
            
            //use Strategy pattern instead of function
             SendTelegramNotification(zaiavka);


            async Task SendTelegramNotification(Zaiavka Newzaiavka)
            {
                //chatid should be extracted from configuration instead of hardcoded value
                long chatId = 929453196; // Замените на нужный chatId (где бот должен отправлять сообщения)

                string message = $"Новая заявка!\n" +
                                 $"ID: {Newzaiavka.id_zv}\n" +
                                 $"Номер кабинета: {Newzaiavka.num_off}\n" +
                                 $"Корпус: {Newzaiavka.parent_off}\n" +
                                 $"Статус: {Newzaiavka.status_zv}\n" +
                                 $"Модель картриджа: {Newzaiavka.cartridg_model}\n";

                await _botClient.SendTextMessageAsync(chatId, message);
            }

            return Redirect($"https://cartridgesmonitoring.azurewebsites.net/Zaiavka/Details/{zaiavka.id_zv}");
        }
        
        // PUT api/<ZaiavkaRestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Zaiavka zaiavka)
        {
            var zv = _context.Zaiavkas.FirstOrDefault(u => u.id_zv == id);
            if (zv != null)
            {
                zv.status_zv = "Закрыта";
               
                _context.Entry(zv).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(zv);
            }
            return NotFound();
        }

        // DELETE api/<ZaiavkaRestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var zaiavka = _context.Zaiavkas.Find(id);

            if (zaiavka != null)
            {
                _context.Remove(zaiavka);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
