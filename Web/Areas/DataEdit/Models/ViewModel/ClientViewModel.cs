using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class ClientViewModel : ICreateViewModel
    {
        public Client EditItem;
        public List<Department> Departments;
        public List<Type> Posts;
    }
}