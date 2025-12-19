using Lab8.Models;
using Microsoft.AspNetCore.Mvc;
using Lab8.Services.Interfaces;

namespace Lab8.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService) => _brandService = brandService;

        public IActionResult Index() => View(_brandService.GetAllBrands());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandService.CreateBrand(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetBrandById(id);
            return brand == null ? NotFound() : View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandService.UpdateBrand(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public IActionResult Delete(int id)
        {
            var brand = _brandService.GetBrandById(id);
            return brand == null ? NotFound() : View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _brandService.DeleteBrand(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


