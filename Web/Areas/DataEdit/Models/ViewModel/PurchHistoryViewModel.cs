using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class PurchHistoryViewModel
    {
        public PurchaseHistory EditItem;
        public List<Client> People;
        public List<Diplom.Models.Model.simple.Type> Status;
        public List<Department> Department;
    }
}