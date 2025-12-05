using Folivora.Scaffold;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using Telegram.Bot;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Проект.Controllers
{
    public class ZaiavkaController : Controller
    {
        private CartrigDbContext _context;
        private ITelegramBotClient _botClient;
        public ZaiavkaController(CartrigDbContext context, ITelegramBotClient botClient)
        {
            _context = context;
            _botClient = botClient;
        }
        // GET: ZaiavkaController
        public ActionResult Index()
        {
            return View(_context.Zaiavkas);
        }

        // GET: ZaiavkaController/Details/5
        public ActionResult Details(int id)
        {
            Zaiavka? zaiavka = new Zaiavka();
            zaiavka = _context.Zaiavkas.FirstOrDefault(x => x.id_zv == id);

            return View(zaiavka);
        }

        // GET: ZaiavkaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZaiavkaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zaiavka zaiavka)
        {
            try
            {
                //string? printerId = HttpContext.Request.Query.FirstOrDefault(q => q.Key == "printer").Value;
                //string? ip = "";
                //if (string.IsNullOrEmpty(printerId))
                //{
                //    ip = Response.HttpContext.Connection.RemoteIpAddress?.ToString();

                //    if (ip == "::1")
                //    {
                //        ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                //    }
                //}
                //else
                //{
                //    ip = _context.Printers.FirstOrDefault(u => u.id_print == int.Parse(printerId))?.ip;
                //}
                //zaiavka.ip_printer = ip?.ToString();
                //Printer? print = _context.Printers.FirstOrDefault(u => u.ip == zaiavka.ip_printer);
                //if (print == null)
                //{
                //    return NotFound();
                //}

                //Office? offices = _context.Offices.FirstOrDefault(u => u.id_off == print.off_id);
                //if (offices == null)
                //{
                //    return NotFound();
                //}
                //zaiavka.num_off = offices.number_off;
                //zaiavka.parent_off = offices.parent;

                //Cartridg? cartridgs = _context.Cartridgs.FirstOrDefault(u => u.id_cr == print.cartridg_id);
                //if (cartridgs == null)
                //{
                //    return NotFound();
                //}
                //zaiavka.cartridg_model = cartridgs.model_cr;
                //zaiavka.status_zv = "Открыта";

                //_context.Zaiavkas.Add(zaiavka);
                //_context.SaveChanges();
                //SendTelegramNotification(zaiavka);

                //async Task SendTelegramNotification(Zaiavka Newzaiavka)
                //{
                //    long chatId = 929453196; 

                //    string message = $"Новая заявка!\n" +
                //                     $"ID: {Newzaiavka.id_zv}\n" +
                //                     $"Номер кабинета: {Newzaiavka.num_off}\n" +
                //                     $"Корпус: {Newzaiavka.parent_off}\n" +
                //                     $"Статус: {Newzaiavka.status_zv}\n" +
                //                     $"Модель картриджа: {Newzaiavka.cartridg_model}\n";

                //    await _botClient.SendTextMessageAsync(chatId, message);
                //}

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZaiavkaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZaiavkaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZaiavkaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZaiavkaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
