using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService;

        public CarController(ICarService carService, ICarModelService carModelService)
        {
            _carService = carService;
            _carModelService = carModelService;
        }

        public IActionResult Index() => View(_carService.GetAllCars());

        public IActionResult Create()
        {
            // Lấy danh sách CarModelVm từ CarModelService
            ViewBag.CarModelId = new SelectList(_carModelService.GetAll(), "Id", "CarModelName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            // Cứ nhận được dữ liệu là đẩy thẳng vào Service, không kiểm tra gì cả
            _carService.CreateCar(car);

            // Lưu xong là té về trang danh sách luôn
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            ModelState.Remove("CarModel");

            if (ModelState.IsValid)
            {
                _carService.UpdateCar(car);
                return RedirectToAction(nameof(Index)); // Lưu xong tự về Index
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

        // 1. Hàm GET: Hiển thị trang xác nhận (Đây là lý do gây lỗi 404 nếu thiếu)
        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }

        // 2. Hàm POST: Thực hiện xóa sau khi người dùng nhấn nút xác nhận
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index)); // Xóa xong quay về danh sách
        }
    }
}