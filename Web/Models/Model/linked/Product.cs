using Diplom.Models.Model.simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models.Model
{
    public class Product
    {
        public Product()
        {
            ProdMovements = new HashSet<ProdMovement>();
        }
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Не введено название товара")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        [Range(0.0, 300.0,ErrorMessage ="Введено неверное количество товара")]
        public int Count { get; set; }
        [Required]
        [Range(1.0,1000000.0, ErrorMessage ="цена находится в недопустимом интервале")]
        public int Price { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string Photo { get; set; }
        public double? Discount { get; set; }
        public DateTime? DiscountDate { get; set; }
        public DateTime AddDate { get; set;}
        [ForeignKey("TypeId")]
        public virtual simple.Type Type { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Department Department { get; set; }
        public virtual Smartphone Smartphone { get; set; }
        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }
        public virtual WirelessHeadphone WirelessHeadphones { get; set; }
        public virtual WireHeadphone WireHeadphones { get; set; }
        public virtual Accessory Accessory { get; set; }
        public virtual Notebook Notebook { get; set; }
        public virtual ICollection<ProdMovement> ProdMovements { get; set; }
    }
}