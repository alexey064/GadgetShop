﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Models.Linked;

namespace Web.Models.Simple
{
    //этот класс используется для обозначения типов для большинства других классов.
    public class Type
    {
        public Type()
        {
            Clients = new HashSet<Client>();
            ProdMovements = new HashSet<ProdMovement>();
            Products = new HashSet<Product>();
            PurchaseHistories = new HashSet<PurchaseHistory>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NormalizeName { get; set; }
        [Required]
        public string Category { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<ProdMovement> ProdMovements { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
    }
}