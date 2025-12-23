using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // GET: Order
        public IActionResult Index() => View(_orderService.GetAllOrders());

        // GET: Order/Details/5
        public IActionResult Details(int id)
        {
            // Lấy đơn hàng kèm theo thông tin Khách hàng và Chi tiết các xe đã mua
            var order = _orderService.GetOrderById(id);

            if (order == null) return NotFound();

            // Lấy thêm danh sách các đơn hàng khác của khách hàng này để làm "Lịch sử mua hàng"
            var customerHistory = _orderService.GetAllOrders()
                .Where(o => o.CustomerId == order.CustomerId && o.Id != id)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            ViewBag.CustomerHistory = customerHistory;

            return View(order);
        }

        // GET: Order/Create
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
            // Loại bỏ kiểm tra ModelState cho navigation properties
            ModelState.Remove("Customer");
            ModelState.Remove("OrderDetails");

            if (selectedCars != null && selectedCars.Length > 0)
            {
                order.OrderDate = DateTime.Now;
                order.TotalAmount = 0; // Khởi tạo tổng tiền
                order.Status = 0;      // Mặc định: Chờ duyệt

                foreach (var carId in selectedCars)
                {
                    var car = _carService.GetCarById(carId);
                    if (car != null)
                    {
                        var price = car.Price ?? 0;
                        order.OrderDetails.Add(new OrderDetail
                        {
                            CarId = carId,
                            Price = price,
                            Quantity = 1
                        });
                        order.TotalAmount += price;
                    }
                }
                _orderService.CreateOrder(order);
                TempData["Success"] = "Lập đơn hàng thành công. Vui lòng chờ duyệt!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Vui lòng chọn ít nhất một xe để lập đơn hàng.");
            ViewBag.CustomerId = new SelectList(_customerService.GetAllCustomers(), "Id", "Name", order.CustomerId);
            ViewBag.Cars = _carService.GetAllCars();
            return View(order);
        }

        // Xử lý DUYỆT ĐƠN HÀNG (Approve)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            var order = _orderService.GetOrderById(id);
            // Chỉ duyệt đơn đang ở trạng thái Chờ (0)
            if (order != null && order.Status == 0)
            {
                foreach (var detail in order.OrderDetails)
                {
                    var car = _carService.GetCarById(detail.CarId);
                    if (car != null)
                    {
                        // Kiểm tra tồn kho trước khi trừ
                        if (car.StockQuantity >= detail.Quantity)
                        {
                            car.StockQuantity -= detail.Quantity;
                            _carService.UpdateCar(car);
                        }
                        else
                        {
                            TempData["Error"] = $"Xe {car.Name} không đủ số lượng trong kho!";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }

                order.Status = 1; // Cập nhật trạng thái: Đã duyệt (1)
                _orderService.UpdateOrder(order);

                TempData["Success"] = $"Đã duyệt đơn hàng #{id} và cập nhật kho thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        // Xử lý HỦY ĐƠN HÀNG (Cancel)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order != null && order.Status == 0)
            {
                order.Status = 2; // Cập nhật trạng thái: Đã hủy (2)
                _orderService.UpdateOrder(order);
                TempData["Warning"] = $"Đơn hàng #{id} đã bị hủy.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Delete/5
        public IActionResult Delete(int id) => View(_orderService.GetOrderById(id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}