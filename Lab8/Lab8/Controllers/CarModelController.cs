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

        public IActionResult Index() => View(_carModelService.GetAll());

        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            // Ép kiểu BrandId về int để chắc chắn không bị null
            if (carModel.BrandId > 0)
            {
                _carModelService.Create(carModel);
                return RedirectToAction(nameof(Index)); // Chuyển hướng ngay lập tức
            }

            // Nếu vẫn lỗi, nạp lại dữ liệu cho Dropdown
            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
            return View(carModel);
        }

        public IActionResult Edit(int id)
        {
            var model = _carModelService.GetById(id);
            if (model == null) return NotFound();
            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", model.BrandId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel carModel)
        {
            // Loại bỏ kiểm tra lỗi cho thuộc tính điều hướng "Brand"
            ModelState.Remove("Brand");

            if (ModelState.IsValid)
            {
                _carModelService.Update(carModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BrandId = new SelectList(_brandService.GetAllBrands(), "Id", "Name", carModel.BrandId);
            return View(carModel);
        }

     

        // 1. GET: Hiển thị trang xác nhận xóa
        public IActionResult Delete(int id)
        {
            var model = _carModelService.GetById(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // 2. POST: Thực hiện xóa khi người dùng nhấn nút xác nhận
        [HttpPost, ActionName("Delete")] // Quan trọng: ActionName giúp khớp với asp-action="Delete" ở View
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carModelService.Delete(id);
            return RedirectToAction(nameof(Index)); // Xóa xong tự động quay về danh sách
        }
    }
}