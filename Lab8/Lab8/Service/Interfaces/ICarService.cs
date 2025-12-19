using Lab8.Models;

namespace Lab8.Services.Interfaces
{
    public interface ICarService
    {
      List<Car> GetAllCars();
        Car? GetCarById(int id); 
        void CreateCar(Car car);
       void UpdateCar(Car car); 
       void DeleteCar(int id); 
    }
}