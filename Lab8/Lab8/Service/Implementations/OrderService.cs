using Lab8.Models;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementations
{
    public class OrderService : IOrderService
    {
        // 1. Khai báo biến toàn cục trong lớp (Sửa lỗi CS0103)
        private readonly IOrderRepository _orderRepository;

        // 2. Gán giá trị thông qua Constructor
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders() => _orderRepository.GetAll();

        public Order? GetOrderById(int id) => _orderRepository.GetById(id);

        public void CreateOrder(Order order) => _orderRepository.Add(order);

        public void DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}