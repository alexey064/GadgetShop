using Web.Models.Interfaces;
using System.Collections.Generic;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.ViewModel
{
    public class ClientViewModel : ICreateViewModel
    {
        public Client EditItem;
        public List<Department> Departments;
        public List<Type> Posts;
    }
}