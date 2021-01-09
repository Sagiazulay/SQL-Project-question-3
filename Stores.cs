using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROJECT2
{
    class Stores
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Category_ID { get; set; }

        public Stores()
        {

        }
        public Stores(int iD, string name, int floor, int category_ID)
        {
            ID = iD;
            Name = name;
            Floor = floor;
            Category_ID = category_ID;
        }
        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
        public override bool Equals(object obj)
        {
            return this.ID == ((Stores)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Stores c1, Stores c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Stores c1, Stores c2)
        {
            return !(c1 == c2);
        }

    }
}
