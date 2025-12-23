using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Đổi từ GetAll() thành GetAllCustomers() cho khớp Interface
        public IActionResult Index() => View(_customerService.GetAllCustomers());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer); // Dùng đúng tên hàm trong Interface
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            // Sử dụng đúng tên hàm GetCustomerById mà bạn đã định nghĩa trong Interface
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer); // Hệ thống sẽ tìm file Views/Customer/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Đổi thành UpdateCustomer
                _customerService.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int id)
        {
            // Tìm khách hàng để hiển thị thông tin trước khi xóa
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Gọi hàm xóa từ Service
            _customerService.DeleteCustomer(id);

            // Xóa xong quay về danh sách
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}