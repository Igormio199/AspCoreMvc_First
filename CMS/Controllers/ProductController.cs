using System.Diagnostics;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
   
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ProductController(ApplicationDbContext context)
        {
            _context = context;
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //    Product product1 = new Product { Name = "Samsung", Price = 45000 };
            //    Product product2 = new Product { Name = "IPhone", Price = 55000 };
            //    Product product3 = new Product { Name = "Honor", Price = 30000 };
            //    Product product4 = new Product { Name = "Fly", Price = 15000 };
            //    Product product5 = new Product { Name = "Nokia", Price = 23000 };
            //    _context.Products.AddRange(product1, product2, product3, product4, product5);
            _context.SaveChanges();

        }


        public IActionResult Index()
        {
           
                //_context.Database.EnsureCreated();
                var products = _context.Products.ToList();
                _context.SaveChanges();
                return View(products);
        }
    }
}
