using Lab8.Data;
using Lab8.Models;
using Lab8.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab8.Repository.Implementations
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ApplicationDbContext _context;

        public CarModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CarModel> GetAll()
        {
            // Sử dụng Include để lấy dữ liệu từ các bảng liên quan
            return _context.CarModels
                .Include(m => m.Brand) // Lấy thông tin Brand
                .Include(m => m.Cars)  // Lấy danh sách phiên bản xe
                .ToList();
        }

        public CarModel? GetById(int id)
        {
            return _context.CarModels
                .Include(m => m.Brand)
                .Include(m => m.Cars)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Add(CarModel model)
        {
            _context.CarModels.Add(model);
            _context.SaveChanges();
        }

        public void Update(CarModel model)
        {
            _context.CarModels.Update(model);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var model = _context.CarModels.Find(id);
            if (model != null)
            {
                _context.CarModels.Remove(model);
                _context.SaveChanges();
            }
        }
    }
}