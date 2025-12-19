using Lab8.Models;

namespace Lab8.Services.Interfaces
{
    public interface ICarModelService
    {
        List<CarModelVm> GetAll(); 
        CarModel? GetById(int id);
        void Create(CarModel model);
        void Update(CarModel model);
        void Delete(int id);
    }
}