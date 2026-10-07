using Microsoft.AspNetCore.Mvc;
using entasp.Data;
using entasp.Models;

namespace entasp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // CUSTOMER LIST
        public IActionResult Index()
        {
            var customers = _db.Customers.ToList();
            return View(customers);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // CREATE - show form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - save customer
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT - show form
        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // EDIT - save changes
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE - remove customer
        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}