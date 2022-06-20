using System.ComponentModel.DataAnnotations;
using Web.Models.Simple;

namespace Web.Models.Linked
{
    public class Client
    {
        public Client()
        {
        }
        [Key]
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
        public virtual Simple.Type Post { get; set; }
    }
}