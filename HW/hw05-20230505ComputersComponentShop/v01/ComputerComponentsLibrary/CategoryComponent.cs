using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComponentsLibrary
{
    public class CategoryComponent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //public static CategoryComponent GetCategoryById(List<CategoryComponent> categiriesList, int id)
        //{
        //    return (CategoryComponent) categiriesList.Where(c => c.Id == id);
        //}
    }
}
