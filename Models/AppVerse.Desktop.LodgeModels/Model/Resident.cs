using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Desktop.LodgeModels.Model
{
  public  class Resident
    {

      public Guid ID { get; set; }

      public string FirstName { get; set; }
      public string LastName { get; set; }

      public string MiddleName { get; set; }

      public DateTime DateOfBirth { get; set; }

      public DateTime CreatedOn { get; set; }

      public string CreatedBy { get; set; }

    }
}
