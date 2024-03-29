﻿namespace AppVerse.Conference.MsSql.EntityMapping;

public record RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> modelBuilder)
    {
        modelBuilder.Property(e => e.CreationDate).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.HasOne(e => e.Profile);
    }
}
