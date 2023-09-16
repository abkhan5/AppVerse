namespace AppVerse.Conference.MsSql.EntityMapping;
public record AppVerseUserMapping : IEntityTypeConfiguration<AppVerseUser>
{
    public void Configure(EntityTypeBuilder<AppVerseUser> modelBuilder)
    {
        modelBuilder.Property(e => e.CreatedOn).HasDefaultValue(DateTime.UtcNow);

        modelBuilder.Property(e => e.ReferralCode).HasMaxLength(256).IsRequired();
        modelBuilder.HasIndex(e => e.ReferralCode).HasDatabaseName("ReferralCodeIndex").IsUnique();


        modelBuilder.Property(e => e.UserName).HasMaxLength(256);

        modelBuilder.Property(e => e.NormalizedUserName).HasMaxLength(256);
        modelBuilder.HasIndex(e => e.NormalizedUserName).HasDatabaseName("NormalizedUserNameIndex").IsUnique();

        modelBuilder.Property(e => e.Email).HasMaxLength(256);
        modelBuilder.HasIndex(e => e.Email).HasDatabaseName("EmailIndex").IsUnique();

        modelBuilder.Property(e => e.NormalizedEmail).HasMaxLength(256);
        modelBuilder.HasIndex(e => e.NormalizedEmail).HasDatabaseName("NormalizedEmailIndex").IsUnique();

        modelBuilder.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();     
    }
}