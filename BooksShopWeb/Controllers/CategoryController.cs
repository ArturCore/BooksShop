using BooksShop.DataAccess.Data;
using BooksShop.DataAccess.Repository.IRepository;
using BooksShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromdb = _categoryRepo.Get(c => c.Id == id);
            if (categoryFromdb == null)
            {
                return NotFound();
            }
            return View(categoryFromdb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
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
            Category? categoryFromdb = _categoryRepo.Get(c => c.Id == id);
            if (categoryFromdb == null)
            {
                return NotFound();
            }
            return View(categoryFromdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category categoryToDelete = _categoryRepo.Get(c => c.Id == id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(categoryToDelete);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
