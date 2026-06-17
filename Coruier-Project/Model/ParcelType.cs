using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coruier_Project
{
    public class ParcelType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        //1:M
        public ICollection<Parcel> parcels { get; set; }
    }
}

