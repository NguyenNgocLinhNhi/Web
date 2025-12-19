using Lab8.Models;

namespace Lab8.Repository.Interfaces
{
    public interface ICarModelRepository
    {
       List<CarModelVm> GetAll(); 
        CarModel? GetById(int id);
        void Add(CarModel carModel); 
       void Update(CarModel carModel);
         void Delete(int id); 
    }
}