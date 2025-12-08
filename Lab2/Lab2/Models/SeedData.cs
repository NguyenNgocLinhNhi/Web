using Lab2.Models;
using Lab2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Lab2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Lab2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Lab2Context>>()))
            {
                // -----------------------------------------------------------
                // 1. SEED DATA CHO MOVIE (Dữ liệu gốc của bạn)
                // -----------------------------------------------------------

                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M,
                            Rating = "PG" // Thêm Rating cho dữ liệu mẫu
                        },
                        new Movie
                        {
                            Title = "Ghostbusters",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M,
                            Rating = "PG"
                        },
                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M,
                            Rating = "PG"
                        },
                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M,
                            Rating = "G"
                        }
                    );
                }

                // -----------------------------------------------------------
                // 2. SEED DATA CHO CATEGORY (Phân loại)
                // -----------------------------------------------------------

                if (!context.Category.Any())
                {
                    var categories = new Category[]
                    {
                        new Category { Name = "Technical Support" },
                        new Category { Name = "Billing Inquiry" },
                        new Category { Name = "Feature Request" },
                        new Category { Name = "Bug Report" }
                    };
                    context.Category.AddRange(categories);
                }
                context.SaveChanges(); // Lưu Categories trước để có ID

                // -----------------------------------------------------------
                // 3. SEED DATA CHO CUSTOMER (Khách hàng)
                // -----------------------------------------------------------

                if (!context.Customer.Any())
                {
                    var customers = new Customer[]
                    {
                        new Customer { Name = "Alice Johnson", Email = "alice@example.com", Phone = "0901234567" },
                        new Customer { Name = "Bob Smith", Email = "bob.s@company.net", Phone = "0917654321" },
                        new Customer { Name = "Charlie Brown", Email = "charlie@freelance.org", Phone = "0889900112" }
                    };
                    context.Customer.AddRange(customers);
                }
                context.SaveChanges(); // Lưu Customers trước để có ID

                // -----------------------------------------------------------
                // 4. SEED DATA CHO TICKET (Sự cố)
                // -----------------------------------------------------------

                if (!context.Ticket.Any())
                {
                    // Truy vấn lại các đối tượng đã được lưu để lấy ID
                    var existingCategories = context.Category.ToList();
                    var existingCustomers = context.Customer.ToList();

                    // Dùng LINQ để tìm ID chính xác
                    var bugReportId = existingCategories.FirstOrDefault(c => c.Name == "Bug Report")!.Id;
                    var techSupportId = existingCategories.FirstOrDefault(c => c.Name == "Technical Support")!.Id;
                    var featureRequestId = existingCategories.FirstOrDefault(c => c.Name == "Feature Request")!.Id;
                    var billingInquiryId = existingCategories.FirstOrDefault(c => c.Name == "Billing Inquiry")!.Id;

                    var tickets = new Ticket[]
                    {
                        new Ticket
                        {
                            Subject = "Cannot login after update",
                            Description = "I am receiving a 403 error when trying to log into the application after the latest release.",
                            CreatedDate = DateTime.Now.AddDays(-5),
                            Status = "Open",
                            CustomerId = existingCustomers[0].Id, // Alice Johnson
                            CategoryId = bugReportId
                        },
                        new Ticket
                        {
                            Subject = "API response slow",
                            Description = "The data API is taking over 5 seconds to respond during peak hours.",
                            CreatedDate = DateTime.Now.AddDays(-2),
                            Status = "In Progress",
                            CustomerId = existingCustomers[1].Id, // Bob Smith
                            CategoryId = techSupportId
                        },
                        new Ticket
                        {
                            Subject = "Request dark mode",
                            Description = "Please add a dark theme option for better nighttime viewing.",
                            CreatedDate = DateTime.Now.AddDays(-1),
                            Status = "Open",
                            CustomerId = existingCustomers[2].Id, // Charlie Brown
                            CategoryId = featureRequestId
                        },
                        new Ticket
                        {
                            Subject = "Invoice 101 incorrect amount",
                            Description = "The calculated tax on invoice #101 appears to be wrong.",
                            CreatedDate = DateTime.Now.AddDays(-10),
                            Status = "Closed",
                            CustomerId = existingCustomers[0].Id, // Alice Johnson
                            CategoryId = billingInquiryId
                        }
                    };
                    context.Ticket.AddRange(tickets);
                }

                context.SaveChanges(); // Lưu tất cả những thay đổi còn lại
            }
        }
    }
}