using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using System.Collections.Generic;

namespace Diplom.Models.ViewModel
{
    public class NotebookViewModel
    {
        public Notebook EditItem;
        public Dictionary<int, string> Brands;
        public Dictionary<int, string> Departments;
        public Dictionary<int, string> Types;
        public Dictionary<int, string> Processors;
        public Dictionary<int, string> ScreenTypes;
        public Dictionary<int, string> OS;
        public Dictionary<int, string> Colors;
        public Dictionary<int, string> Videocards;
    }
}
