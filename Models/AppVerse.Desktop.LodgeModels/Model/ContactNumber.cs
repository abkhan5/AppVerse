namespace AppVerse.Desktop.LodgeModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patna.ContactNumber")]
    public partial class ContactNumber
    {
        public int ID { get; set; }

        public int NumberType { get; set; }

        public int PhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ResidentId { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }

        public virtual Resident Resident { get; set; }
    }
}
