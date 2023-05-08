using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComponentsLibrary
{
    public class Component
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public CategoryComponent Category { get; set; }

        public Component(int id, string title, double price, CategoryComponent category)
        {
            Id = id;
            Title = title;
            Price = price;
            Category = category;
        }
    }
}
