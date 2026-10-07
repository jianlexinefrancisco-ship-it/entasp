using Microsoft.AspNetCore.Mvc;
using entasp.Data;
using entasp.Models;

namespace entasp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                item.Quantity = quantity;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}