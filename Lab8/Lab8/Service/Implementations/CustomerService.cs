using Lab8.Models;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;
using System.Collections.Generic;

namespace Lab8.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public List<Customer> GetAllCustomers() => _repository.GetAll();
        public Customer? GetCustomerById(int id) => _repository.GetById(id);
        public void CreateCustomer(Customer customer) => _repository.Add(customer);
        public void UpdateCustomer(Customer customer) => _repository.Update(customer);
        public void DeleteCustomer(int id) => _repository.Delete(id);
    }
}