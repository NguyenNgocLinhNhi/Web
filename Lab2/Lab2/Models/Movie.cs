using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class Movie
    {
        // ID sẽ là Primary Key mặc định của EF Core
        public int Id { get; set; }

        public string? Title { get; set; }

        // Sử dụng Data Annotation để chỉ định đây là kiểu Date.
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string? Genre { get; set; }

        // Cấu hình kiểu dữ liệu chính xác cho cột Price trong SQL Server
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}