namespace AppVerse.Desktop.LodgeModels.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patna.ResidentHistory")]
    public partial class ResidentHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ResidentId { get; set; }

        public int PaymentAmount { get; set; }

        public string Comment { get; set; }

        public virtual Resident Resident { get; set; }
    }
}
