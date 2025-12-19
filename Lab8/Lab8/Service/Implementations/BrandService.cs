using Lab8.Models;
using Lab8.Repository.Implementations;
using Lab8.Repository.Interfaces;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository; 
        }

       public List<Brand> GetAllBrands() => _brandRepository.GetAll();
        public Brand? GetBrandById(int id) => _brandRepository.GetById(id); 
       public void CreateBrand(Brand brand) => _brandRepository.Add(brand);
        public void UpdateBrand(Brand brand) => _brandRepository.Update(brand); 
      public void DeleteBrand(int id) => _brandRepository.Delete(id); 
    }
}