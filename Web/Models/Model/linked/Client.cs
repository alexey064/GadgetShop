using Diplom.Models.Model.simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Diplom.Models.Model
{
    public class Client
    {
        public Client()
        {

        }

        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public int PostId { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual simple.Type Post { get; set; }

        //public virtual ICollection<PurchaseHistory> PurchaseHistoryClients { get; set; }
    }
}