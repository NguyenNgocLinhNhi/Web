using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting; // Thêm để dùng IWebHostEnvironment
using Microsoft.AspNetCore.Http;    // Thêm để dùng IFormFile
using System.IO;

namespace Lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService;
        private readonly IWebHostEnvironment _hostEnvironment; // Để lấy đường dẫn thư mục wwwroot

        public CarController(ICarService carService, ICarModelService carModelService, IWebHostEnvironment hostEnvironment)
        {
            _carService = carService;
            _carModelService = carModelService;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index() => View(_carService.GetAllCars());

        public IActionResult Create()
        {
            ViewBag.CarModelId = new SelectList(_carModelService.GetAll(), "Id", "CarModelName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car, IFormFile? ImageFile)
        {
            // Loại bỏ kiểm tra Navigation property để ModelState không bị False vô lý
            ModelState.Remove("CarModel");

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (ImageFile != null)
                {
                    car.ImageUrl = await SaveImage(ImageFile);
                }

                _carService.CreateCar(car);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarModelId = new SelectList(_carModelService.GetAll(), "Id", "CarModelName", car.CarModelId);
            return View(car);
        }

        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            ViewBag.CarModelId = new SelectList(_carModelService.GetAll(), "Id", "CarModelName", car.CarModelId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Car car, IFormFile? ImageFile)
        {
            ModelState.Remove("CarModel");

            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    // Nếu có ảnh mới, lưu ảnh mới và cập nhật đường dẫn
                    car.ImageUrl = await SaveImage(ImageFile);
                }
                // Nếu không chọn ảnh mới, ImageUrl sẽ giữ nguyên giá trị từ input hidden trong View

                _carService.UpdateCar(car);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarModelId = new SelectList(_carModelService.GetAll(), "Id", "CarModelName", car.CarModelId);
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }

        // Hàm phụ dùng chung để lưu file ảnh
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string path = Path.Combine(wwwRootPath, "images/cars");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fullPath = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/cars/" + fileName;
        }
    }
}