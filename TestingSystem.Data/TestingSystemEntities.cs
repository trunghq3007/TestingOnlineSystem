using System.Data.Entity;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data
{
    class TestingSystemEntities:DbContext,IUnitOfWork
    {
        public TestingSystemEntities() : base("TestingSystem")
        {
        }
       

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configtion

        }






        public DbSet<Group> Groups { get; set; }


    }
}
