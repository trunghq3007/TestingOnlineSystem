using System.Data.Entity;
using TestingSystem.Data;

namespace TestingSystem.App_Start
{
    public class DatabaseSetup
    {
        public static void Initialize()
        {
            //Database.SetInitializer(new SeedData());

            using (var db = new TestingSystemEntities())
            {
                db.Database.Initialize(true);
            }
        }
    }
}