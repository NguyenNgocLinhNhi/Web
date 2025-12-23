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

        // Triển khai phương thức Update
        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToList();
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
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}