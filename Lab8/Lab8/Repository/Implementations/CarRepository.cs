using Lab8.Data;
using Lab8.Models;
using Lab8.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Repository.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context) => _context = context;

        public List<Car> GetAll()
        {
            return _context.Cars
                .Include(c => c.CarModel!)
                    .ThenInclude(m => m.Brand)
                .AsNoTracking().ToList();
        }

        public Car? GetById(int id)
        {
            return _context.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            var local = _context.Cars.Local.FirstOrDefault(c => c.Id == car.Id);
            if (local != null) _context.Entry(local).State = EntityState.Detached;

            _context.Entry(car).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null) { _context.Cars.Remove(car); _context.SaveChanges(); }
        }
    }
}