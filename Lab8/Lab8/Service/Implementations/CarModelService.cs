using Lab8.Models;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;
using System.Collections.Generic;

namespace Lab8.Services.Implementations
{
    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _repository;

        public CarModelService(ICarModelRepository repository)
        {
            _repository = repository;
        }

        // Đổi kiểu trả về thành CarModel để khớp với Interface mới
        public List<CarModel> GetAll() => _repository.GetAll();
        public CarModel? GetById(int id) => _repository.GetById(id);
        public void Create(CarModel model) => _repository.Add(model);
        public void Update(CarModel model) => _repository.Update(model);
        public void Delete(int id) => _repository.Delete(id);
    }
}