using Microsoft.AspNetCore.Mvc;
using entasp.Data;
using entasp.Models;

namespace entasp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

      public IActionResult Index(string searchString)
{
    var products = _db.Products.AsQueryable();

    if (!string.IsNullOrEmpty(searchString))
    {
        products = products.Where(p =>
            p.Name.ToLower().Contains(searchString.ToLower()));
    }

    ViewData["searchString"] = searchString;

    return View(products.ToList());
}
        // CREATE - show the add form
public IActionResult Create()
{
    return View();
}

// CREATE - save the new product
[HttpPost]
public IActionResult Create(Product product)
{
    product.Description = product.Description ?? "";
    product.Category = product.Category ?? "";

    _db.Products.Add(product);
    _db.SaveChanges();

    return RedirectToAction("Index");
}
        // EDIT - show the edit form
public IActionResult Edit(int id)
{
    var product = _db.Products.Find(id);

    if (product == null)
        return RedirectToAction("Index");

    return View(product);
}

// EDIT - save the changes
[HttpPost]
public IActionResult Edit(Product product)
{
    var existingProduct = _db.Products.Find(product.Id);

    if (existingProduct == null)
    {
        return RedirectToAction("Index");
    }

    existingProduct.Name = product.Name;
    existingProduct.Description = product.Description ?? "";
    existingProduct.Price = product.Price;
    existingProduct.Category = product.Category ?? "";

    _db.SaveChanges();

    return RedirectToAction("Index");
}

// DELETE - remove the product
public IActionResult Delete(int id)
{
    var product = _db.Products.Find(id);

    if (product != null)
    {
        _db.Products.Remove(product);
        _db.SaveChanges();
    }

    return RedirectToAction("Index");
}
public IActionResult AddToCart(int id)
{
    var product = _db.Products.Find(id);

    if (product != null)
    {
        var cartItem = new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1
        };

        _db.CartItems.Add(cartItem);
        _db.SaveChanges();
    }

    return RedirectToAction("Index");
}
    }
}