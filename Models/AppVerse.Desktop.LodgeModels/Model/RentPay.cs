using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Desktop.LodgeModels.Model
{
 public class RentPay
    {
     public Guid ID { get; set; }
     public DateTime CreatedOn { get; set; }
     public Guid CreatedBy { get; set; }
    }
}
