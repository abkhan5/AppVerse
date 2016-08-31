namespace AppVerse.Desktop.LodgeModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patna.ContactUploads")]
    public partial class ContactUpload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ResidentId { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] ContactScan { get; set; }

        public virtual Resident Resident { get; set; }
    }
}
