using Diplom.Models.Interfaces;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using System.Collections.Generic;
namespace Diplom.Models.ViewModel
{
    public class NotebookViewModel
    {
        public Notebook EditItem;
        public List<Brand> Brands;
        public List<Department> Departments;
        public List<Type> Types;
        public List<Processor> Processors;
        public List<ScreenType> ScreenTypes;
        public List<OS> OS;
        public List<Color> Colors;
        public List<Videocard> Videocards;
    }
}