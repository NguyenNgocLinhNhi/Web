using Lab8.Models;
using System.Collections.Generic;

namespace Lab8.Repository.Interfaces
{
    public interface ICarModelRepository
    {
        List<CarModel> GetAll();
        CarModel? GetById(int id);
        void Add(CarModel model);
        void Update(CarModel model);
        void Delete(int id);
    }
}