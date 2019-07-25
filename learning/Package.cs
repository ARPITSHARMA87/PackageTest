using System;
using SQLite;

namespace learning
{
    public class Package
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }

        public override string ToString()
        {
            return $"package item [barcode:{Barcode}, width:{Width}, height:{Height}, depth:{Depth}]";
        }

        public Package()
        {
        }
    }
}
