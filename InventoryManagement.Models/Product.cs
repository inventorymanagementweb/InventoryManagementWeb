using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Models.Interface;

namespace InventoryManagement.Models
{
    [Table("Products")]
    public class Product : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public string Sku { get; set; }
        public double Price { get; set; }
        public int ReorderPoint { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public string BarcodeNumber { get; set; }
        public string Manufacture { get; set; }
        public string Origin { get; set; }
    }
}
