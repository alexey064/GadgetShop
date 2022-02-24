using Diplom.Models.Model.simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models.Model
{
    public class PurchaseHistory
    {
        public PurchaseHistory()
        { 
        }
        public int Id { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        public int? ClientId { get; set; }
        public int? SellerId { get; set; }
        public int StatusId { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public int TotalCost { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        [ForeignKey("SellerId")]
        public virtual Client Seller { get; set; }
        [ForeignKey("StatusId")]
        public simple.Type Status { get; set; }
        public virtual Department department { get; set; }
        //public virtual int ProdMovementId { get; set; }
        public virtual List<ProdMovement> ProdMovement { get; set; }
    }
}