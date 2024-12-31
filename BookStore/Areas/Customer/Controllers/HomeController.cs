using Store.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Store.DataAccess.Repository.IRepository;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository product_repo;

        public HomeController(ILogger<HomeController> logger,IProductRepository product_repo)
        {
            _logger = logger;
            this.product_repo = product_repo;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = product_repo.GetAll(includeProperties: "category");
            return View(productlist);
        }
        public IActionResult Details(int id)
        {
            var product = product_repo.GetSingle(a => a.Id==id,includeProperties: "category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
