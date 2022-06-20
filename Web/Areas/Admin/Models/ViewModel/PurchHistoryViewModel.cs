using System.Collections.Generic;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Models.ViewModel
{
    public class PurchHistoryViewModel
    {
        public PurchaseHistory EditItem;
        public List<Client> People;
        public List<Simple.Type> Status;
        public List<Department> Department;
    }
}