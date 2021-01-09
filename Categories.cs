using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROJECT2
{
    class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public Categories()
        {

        }
        public Categories(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
    

        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }

        public override bool Equals(object obj)
        {
            return this.ID == ((Categories)obj).ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }
        public static bool operator ==(Categories c1, Categories c2)
        {
            return c1.ID == c2.ID;
        }
        public static bool operator !=(Categories c1, Categories c2)
        {
            return !(c1 == c2);
        }
    }
}
