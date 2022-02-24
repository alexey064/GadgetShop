using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class ClientViewModel : ICreateViewModel
    {
        public Client EditItem;
        public Dictionary<int, string> Departments;
        public Dictionary<int, string> Posts;
    }
}