using Lab8.Data;
using Lab8.Models;
using Lab8.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Repository.Implementations
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ApplicationDbContext _context;
        public CarModelRepository(ApplicationDbContext context) => _context = context;

        public List<CarModelVm> GetAll()
        {
            return _context.CarModels
                .Include(cm => cm.Brand)
                .Select(cm => new CarModelVm
                {
                    Id = cm.Id,
                    CarModelName = cm.Name,
                    BrandId = cm.BrandId,
                    BrandName = cm.Brand != null ? cm.Brand.Name : "N/A"
                })
                .AsNoTracking().ToList();
        }

        public CarModel? GetById(int id) =>
            _context.CarModels.Include(cm => cm.Brand).FirstOrDefault(cm => cm.Id == id);

        public void Add(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            _context.SaveChanges(); // Lưu mới
        }

        public void Update(CarModel carModel)
        {
            // 1. Kiểm tra xem trong bộ nhớ đệm (Local) có thực thể này chưa để tránh lỗi "already tracked"
            var local = _context.CarModels.Local.FirstOrDefault(m => m.Id == carModel.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            // 2. Gán trạng thái Modified để EF hiểu đây là lệnh Cập nhật bản ghi cũ
            _context.Entry(carModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Tìm trực tiếp từ Database
            var carModel = _context.CarModels.Find(id);
            if (carModel != null)
            {
                _context.CarModels.Remove(carModel);
                _context.SaveChanges();
            }
        }
    }
}