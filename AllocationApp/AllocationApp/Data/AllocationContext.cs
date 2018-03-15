using AllocationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Data
{
    public class AllocationContext : DbContext
    {
        //this constructor is needed as when the context is being added in the ConfigureServices method in 
        //startup, it takes the options specified in the startup (actually the SetupConfiguration method in program.cs) 
        //and passes them into the context o that it knows what connection string to use 
        public AllocationContext(DbContextOptions<AllocationContext> options): base(options)
        {

        }
        public DbSet<Subordinates> Subordinates { get; set; }
    }
}
