using Folivora.Scaffold;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Проект.Controllers
{
    public class CartridgController : Controller
    {
        private CartrigDbContext _context;
        public CartridgController(CartrigDbContext context)
        {
            _context = context;
        }
        // GET: CartridgController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartridgController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartridgController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartridgController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CartridgController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartridgController/Edit/5
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

        // GET: CartridgController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartridgController/Delete/5
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
