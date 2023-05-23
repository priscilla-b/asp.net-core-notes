using Microsoft.AspNetCore.Mvc;
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
            _db.Categories.Add(obj);
            // the category object (obj) is the instace of the 
            // category model filled by the user

            _db.SaveChanges();
            return RedirectToAction("Index");  
        }
    }
}
