using Microsoft.AspNetCore.Mvc;
using System.Runtime.Remoting;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        // why is this constructor necessary??

        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to prevent csrf attacks
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                // the category object (obj) is the instace of the 
                // category model filled by the user

                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            
            return View(obj);
            
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            { 
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        // to prevent csrf attacks
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);

                _db.SaveChanges();

                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
                

            }

            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // to prevent csrf attacks
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound(id);
            }
            
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
