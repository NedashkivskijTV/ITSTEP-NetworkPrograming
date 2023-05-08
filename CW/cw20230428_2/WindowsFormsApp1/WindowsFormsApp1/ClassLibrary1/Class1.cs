using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Pochta
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }

        public Pochta(int ID, string Name, string Index) {
            this.ID = ID;
            this.Name = Name;
            this.Index = Index;
        }

        public override string ToString()
        {
            return $"{ID} | {Name} | {Index}";
        }
    }
}
