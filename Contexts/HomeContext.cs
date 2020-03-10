using Microsoft.EntityFrameworkCore;
using Dispatch.Models;
namespace Dispatch.Contexts
{
    public class HomeContext :DbContext
    {
        public HomeContext (DbContextOptions options) : base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<Incident> Incidents {get;set;}
        public DbSet<Rescuer> Rescuers {get;set;}
        public DbSet<Unit> Units {get;set;}
        public DbSet<Dispatchh> Dispatches {get;set;}
        public DbSet<Assignment> Assignments {get;set;}

    }
}