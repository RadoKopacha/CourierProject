using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Coruier_Project
{
    public class ParcelContext : DbContext
    {
        public ParcelContext() : base("ParcelContext")
        {

        }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<ParcelType> parcelTypes { get; set; }


    }
}
