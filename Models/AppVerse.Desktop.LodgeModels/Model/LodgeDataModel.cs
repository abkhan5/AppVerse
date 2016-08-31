namespace AppVerse.Desktop.LodgeModels.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LodgeDataModel : DbContext
    {
        public LodgeDataModel()
            : base("name=LodgeModel")
        {
        }

        public virtual DbSet<ContactNumber> ContactNumbers { get; set; }
        public virtual DbSet<ContactUpload> ContactUploads { get; set; }
        public virtual DbSet<Resident> Residents { get; set; }
        public virtual DbSet<ResidentHistory> ResidentHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resident>()
                .HasMany(e => e.ContactNumbers)
                .WithRequired(e => e.Resident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resident>()
                .HasMany(e => e.ContactUploads)
                .WithRequired(e => e.Resident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resident>()
                .HasMany(e => e.ResidentHistories)
                .WithRequired(e => e.Resident)
                .WillCascadeOnDelete(false);
        }
    }
}
