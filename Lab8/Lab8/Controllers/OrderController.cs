using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;

        public OrderController(IOrderService orderService, ICustomerService customerService, ICarService carService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _carService = carService;
        }

        public IActionResult Index() => View(_orderService.GetAllOrders());

        public IActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "Id", "Name");
            ViewBag.Cars = _carService.GetAllCars();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order, int[] selectedCars)
        {
            if (selectedCars != null && selectedCars.Length > 0)
            {
                order.OrderDate = DateTime.Now;
                foreach (var carId in selectedCars)
                {
                    var car = _carService.GetCarById(carId);
                    if (car != null)
                    {
                        var price = car.Price ?? 0;
                        order.OrderDetails.Add(new OrderDetail { CarId = carId, Price = price });

                        // Gán tổng tiền đơn hàng bằng giá bán của xe
                        order.TotalAmount = price;
                    }
                }
                _orderService.CreateOrder(order);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Vui lòng chọn ít nhất một xe.");
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "Id", "Name");
            ViewBag.Cars = _carService.GetAllCars();
            return View(order);
        }

        public IActionResult Details(int id) => View(_orderService.GetOrderById(id));

        public IActionResult Delete(int id) => View(_orderService.GetOrderById(id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // KHÔNG dùng _context ở đây nữa, dùng Service đã viết ở bước 2
            _orderService.DeleteOrder(id);

            // Quay về trang danh sách sau khi xóa thành công
            return RedirectToAction(nameof(Index));
        }
    }
}