using DataRepository.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class PinContext : DbContext
    {
        //public PinContext()
        //{
        //}

        //public PinContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{
        //}

        //public PinContext(DbConnection existingConnection, bool contextOwnsConnection)
        //    : base(existingConnection, contextOwnsConnection)
        //{
        //}
        //public DbSet<User> User { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<PinContext>(null);
        //    modelBuilder.Configurations.Add(new UserMap());

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
