using Diplom.Models.Model;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class PurchHistoryViewModel
    {
        public PurchaseHistory EditItem;
        public Dictionary<int, string> People;
        public Dictionary<int, string> Status;
        public Dictionary<int, string> Department;
    }
}