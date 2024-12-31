using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository Product_Repo;
        private readonly ICategoryRepository Category_Repo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductRepository Product_Repo,ICategoryRepository Category_Repo, IWebHostEnvironment webHostEnvironment)
        {
            this.Product_Repo = Product_Repo;
            this.Category_Repo = Category_Repo;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> Productls = Product_Repo.GetAll(includeProperties: "category").ToList();
            return View(Productls);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = Category_Repo.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.ID.ToString(),
            });
            //Here I am creating a variable which is IEnumerable meaning it can store objects and using selectListitem which is predefined and has text and value ojects and then I am getting all the values from the repository and using select to get all the values it also used to display values on dropdown list
            ViewBag.CategoryList = CategoryList;
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                var productfromdb = Product_Repo.GetSingle(a => a.Id == id);
                return View(productfromdb);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Product data,IFormFile file)
            {
            string wwwrootpath = webHostEnvironment.WebRootPath;//Here I am getting the webroot path 
           
                if (file != null)
                {
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwrootpath, @"images\product");
                    string fullPath = Path.Combine(ProductPath, filename);
                    if(!string.IsNullOrEmpty(data.ImageURL))//Here I am checking if the image already exists in the form it will then delete the file it is used for the update if we want to update the the image it will delete the previous one and place the new one.
                    {
                        var OldImagePath = Path.Combine(wwwrootpath, data.ImageURL.TrimStart('\\'));  //Here I am getting and triming the oldImagepath In database there is \ and I need to remove it.
                        if(System.IO.File.Exists(OldImagePath))//Checking if the file exists in the system file
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }

                    using (var FileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(FileStream);
                    }
                    data.ImageURL = @"\images\product\" + filename;
                }
                if(data.Id == 0)
                {
                    Product_Repo.Add(data);
                }
                else
                {
                    Product_Repo.Update(data);
                }
                Product_Repo.Save();
                TempData["Success"] = "Product has been created successfully";
                return RedirectToAction("Index");
            
        }
     
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = Product_Repo.GetAll(includeProperties: "category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var ProductToBeDeleted = Product_Repo.GetSingle(a => a.Id == id);
            if (ProductToBeDeleted==null)
            {
                return Json(new { success=false,message="Error While deleting" });
            }
            string wwwrootpath = webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(ProductToBeDeleted.ImageURL))
            {
                var OldImagePath = Path.Combine(wwwrootpath, ProductToBeDeleted.ImageURL.TrimStart('\\'));
                if (System.IO.File.Exists(OldImagePath))
                {
                    System.IO.File.Delete(OldImagePath);
                }
                Product_Repo.Remove(ProductToBeDeleted);
                Product_Repo.Save();
            }
            return Json(new { success = true,message="Delete Successful" });
        }
        #endregion
    }
}
