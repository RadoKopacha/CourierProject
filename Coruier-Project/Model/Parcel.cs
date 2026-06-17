using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coruier_Project
{
    public class Parcel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Wieght { get; set; }

        public int TypeId { get; set; }

        public ParcelType ParcelTypess { get; set; }
    }
}
