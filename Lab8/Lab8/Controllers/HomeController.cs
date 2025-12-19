using System.Diagnostics;
using Lab8.Models;
using Lab8.Services.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Khai báo thêm các Service ?? l?y d? li?u th?ng kê
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public HomeController(
            ILogger<HomeController> logger,
            ICarService carService,
            IBrandService brandService,
            ICustomerService customerService,
            IOrderService orderService)
        {
            _logger = logger;
            _carService = carService;
            _brandService = brandService;
            _customerService = customerService;
            _orderService = orderService;
        }

        // Action Dashboard này s? s?a l?i 404
        public IActionResult Dashboard()
        {
            // L?y s? l??ng th?c t? t? Database thông qua Service
            ViewBag.CarCount = _carService.GetAllCars().Count;
            ViewBag.BrandCount = _brandService.GetAllBrands().Count;
            ViewBag.CustomerCount = _customerService.GetAllCustomers().Count;
            ViewBag.OrderCount = _orderService.GetAllOrders().Count;

            return View();
        }

        public IActionResult Index()
        {
            // N?u b?n mu?n trang ch? m?c ??nh là Dashboard, 
            // có th? chuy?n h??ng Index v? Dashboard
            return RedirectToAction("Dashboard");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}