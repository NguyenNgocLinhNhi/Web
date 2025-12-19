using Lab8.Data;
using Lab8.Models;
using Lab8.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Customer).ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Car)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                // 1. Xóa các dòng chi tiết trước
                _context.OrderDetails.RemoveRange(order.OrderDetails);

                // 2. Xóa đơn hàng chính
                _context.Orders.Remove(order);

                // 3. Lưu thay đổi vào DB
                _context.SaveChanges();
            }
        }
    }
}