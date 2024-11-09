using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication32.Models
{
    public class RegistrationContext : DbContext
    {
       public DbSet<Registration> registration { get; set; }


        public DbSet<Movies> movies { get; set; }

    }
}