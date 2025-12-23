using Lab8.Models;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders() => _orderRepository.GetAll();
        public Order? GetOrderById(int id) => _orderRepository.GetById(id);
        public void CreateOrder(Order order) => _orderRepository.Add(order);

        // Triển khai cập nhật trạng thái đơn hàng
        public void UpdateOrder(Order order) => _orderRepository.Update(order);

        public void DeleteOrder(int id) => _orderRepository.Delete(id);
    }
}