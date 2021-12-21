using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.DataModels
{
    public class StudentAppDataContext:DbContext
    {
       public StudentAppDataContext(DbContextOptions<StudentAppDataContext> options):base(options)
       {
           
       }
        public DbSet<Student> Students { get; set; }
         public DbSet<Gender> Genders { get; set; }
          public DbSet<Address> Addresses { get; set; }
        
        
    }
}