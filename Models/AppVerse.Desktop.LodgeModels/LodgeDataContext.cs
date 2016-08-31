using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVerse.Desktop.LodgeModels.Model;

namespace AppVerse.Desktop.LodgeModels
{
      public class LodgeDataContext : SqlContextBase
    {
        public LodgeDataContext() : base("LodgeDataEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterEntityType(typeof(Resident));
            modelBuilder.RegisterEntityType(typeof(ContactNumber));
            modelBuilder.RegisterEntityType(typeof(ContactUpload));
            modelBuilder.RegisterEntityType(typeof(ResidentHistory));
        }
    }
}
