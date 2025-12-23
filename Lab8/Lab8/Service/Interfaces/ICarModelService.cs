using Lab8.Models;
using System.Collections.Generic; // Thêm thư viện này

namespace Lab8.Services.Interfaces
{
    public interface ICarModelService
    {
        // Đổi CarModelVm thành CarModel
        List<CarModel> GetAll();
        CarModel? GetById(int id);
        void Create(CarModel model);
        void Update(CarModel model);
        void Delete(int id);
    }
}