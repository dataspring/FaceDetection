using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace MemomMvc52.Models
{

    //public class HpbDbContext : DbContext
    //{
        //public HpbDbContext()
        //    : base("name=jhealthConnection")
        //{
        //}   
    //}


    class HpbDbContext : DbContext
    {
        public HpbDbContext(DbConnection connection)
            : base(connection, contextOwnsConnection: true)
        {
            ((IObjectContextAdapter)this).ObjectContext.Connection.Open();
        }

        // …
    }

}