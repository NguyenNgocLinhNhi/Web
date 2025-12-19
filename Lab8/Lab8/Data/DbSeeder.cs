using Lab8.Models;
using Lab8.Data;
using System.Linq;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // 1. Seed dữ liệu Brands (Hãng xe)
        if (!context.Brands.Any())
        {
            var toyota = new Brand { Name = "Toyota", Country = "Japan" };
            var hyundai = new Brand { Name = "Hyundai", Country = "Korea" };
            var ford = new Brand { Name = "Ford", Country = "USA" };
            var vinfast = new Brand { Name = "VinFast", Country = "Vietnam" };

            context.Brands.AddRange(toyota, hyundai, ford, vinfast);
            context.SaveChanges();

            // 2. Seed dữ liệu CarModels (Dòng xe)
            var viosModel = new CarModel { Name = "Vios", BrandId = toyota.Id };
            var accentModel = new CarModel { Name = "Accent", BrandId = hyundai.Id };
            var rangerModel = new CarModel { Name = "Ranger", BrandId = ford.Id };
            var vf8Model = new CarModel { Name = "VF8", BrandId = vinfast.Id };

            context.CarModels.AddRange(viosModel, accentModel, rangerModel, vf8Model);
            context.SaveChanges();

            // 3. Seed dữ liệu Cars (Bổ sung thuộc tính Price để sửa lỗi bạn gặp)
            var car1 = new Car { Name = "Vios G 2024", CarModelId = viosModel.Id, Price = 592000000 };
            var car2 = new Car { Name = "Vios E 2024", CarModelId = viosModel.Id, Price = 458000000 };
            var car3 = new Car { Name = "Accent Blue", CarModelId = accentModel.Id, Price = 542000000 };
            var car4 = new Car { Name = "Ranger Wildtrak", CarModelId = rangerModel.Id, Price = 979000000 };
            var car5 = new Car { Name = "VF8 Plus", CarModelId = vf8Model.Id, Price = 1270000000 };

            context.Cars.AddRange(car1, car2, car3, car4, car5);
            context.SaveChanges();

            // 4. Seed dữ liệu Customer (Khách hàng)
            var customer1 = new Customer { Name = "Nguyễn Văn A", Phone = "0901234567", Email = "vana@gmail.com" };
            var customer2 = new Customer { Name = "Trần Thị B", Phone = "0988777666", Email = "thib@gmail.com" };

            context.Customers.AddRange(customer1, customer2);
            context.SaveChanges();

            // 5. Seed dữ liệu Orders (Đơn hàng mẫu)
            if (!context.Orders.Any())
            {
                var order1 = new Order
                {
                    CustomerId = customer1.Id,
                    OrderDate = DateTime.Now.AddDays(-5),
                    Description = "Khách mua trả góp"
                };
                var order2 = new Order
                {
                    CustomerId = customer2.Id,
                    OrderDate = DateTime.Now.AddDays(-2),
                    Description = "Giao xe tại nhà"
                };

                context.Orders.AddRange(order1, order2);
                context.SaveChanges();

                // 6. Seed dữ liệu OrderDetails (Chi tiết đơn hàng)
                context.OrderDetails.AddRange(
                    new OrderDetail { OrderId = order1.Id, CarId = car1.Id, Price = car1.Price ?? 0 },
                    new OrderDetail { OrderId = order1.Id, CarId = car4.Id, Price = car4.Price ?? 0 },
                    new OrderDetail { OrderId = order2.Id, CarId = car5.Id, Price = car5.Price ?? 0 }
                );
                context.SaveChanges();
            }
        }
    }
}