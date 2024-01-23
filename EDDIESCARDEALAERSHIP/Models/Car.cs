using System;
using System.ComponentModel.DataAnnotations;

namespace EDDIESCARDEALAERSHIP.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Trim { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string? Repairs { get; set; }
        public decimal RepairCost { get; set; }
        public DateTime LotDate { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime? SaleDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Photo { get; set; }
    }
}
