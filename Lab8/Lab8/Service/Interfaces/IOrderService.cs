using Lab8.Models;

namespace Lab8.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void CreateOrder(Order order);
        void DeleteOrder(int id);
    }
}