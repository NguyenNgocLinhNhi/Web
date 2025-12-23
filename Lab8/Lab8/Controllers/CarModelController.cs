using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }

        // GET: CarModel
        public IActionResult Index() => View(_carModelService.GetAll());

        // GET: CarModel/Details/5
        public IActionResult Details(int id)
        {
            var model = _carModelService.GetById(id);
            if (model == null) return NotFound();
            return View(model); // [cite: 14]
        }

        // GET: CarModel/Create
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            // Loại bỏ kiểm tra lỗi cho Navigation Property để tránh lỗi ModelState không hợp lệ
            ModelState.Remove("Brand");
            ModelState.Remove("Cars");

            if (ModelState.IsValid)
            {
                _carModelService.Create(carModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
            return View(carModel);
        }

        // GET: CarModel/Edit/5
        public IActionResult Edit(int id)
        {
            var model = _carModelService.GetById(id);
            if (model == null) return NotFound();

            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", model.BrandId);
            return View(model); // [cite: 24]
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel carModel)
        {
            ModelState.Remove("Brand");
            ModelState.Remove("Cars");

            if (ModelState.IsValid)
            {
                _carModelService.Update(carModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
            return View(carModel);
        }

        // GET: CarModel/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _carModelService.GetById(id);
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: CarModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carModelService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}