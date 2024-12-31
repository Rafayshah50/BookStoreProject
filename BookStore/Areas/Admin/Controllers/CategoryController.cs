using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Utility;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository Category_Repo;

        public CategoryController(ICategoryRepository Category_Repo)
        {
            this.Category_Repo = Category_Repo;
        }
        public IActionResult Index()
        {
            List<Category> Categoryls = Category_Repo.GetAll().ToList();
            return View(Categoryls);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category data)
        {
            if (data.Name == data.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                Category_Repo.Add(data);
                Category_Repo.Save();
                TempData["Success"] = "Category has been created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromdb = Category_Repo.GetSingle(a => a.ID == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }
        [HttpPost]
        public IActionResult Edit(Category data)
        {
            if (data.Name == data.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                Category_Repo.Update(data);
                Category_Repo.Save();
                TempData["Success"] = "Category has been Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromdb = Category_Repo.GetSingle(a => a.ID == id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Deleteconfirm(int? id)
        {
            var dbdata = Category_Repo.GetSingle(a => a.ID == id);
            if (dbdata == null)
            {
                return NotFound();
            }
            Category_Repo.Remove(dbdata);
            Category_Repo.Save();
            TempData["Success"] = "Category has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
