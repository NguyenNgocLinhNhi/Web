using Lab8.Models;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;

namespace Lab8.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;
        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public List<Car> GetAllCars() => _repository.GetAll();
        public Car? GetCarById(int id) => _repository.GetById(id);

        // Khi gọi Add, Car đã bao gồm ImageUrl và Description từ Controller truyền xuống
        public void CreateCar(Car car) => _repository.Add(car);

        public void UpdateCar(Car car) => _repository.Update(car);
        public void DeleteCar(int id) => _repository.Delete(id);
    }
}