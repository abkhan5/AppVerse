namespace AppVerse.Conference.MsSql.EntityMapping;

public record ConferenceMapping : IEntityTypeConfiguration<ConferenceEvent>
{
    public void Configure(EntityTypeBuilder<ConferenceEvent> modelBuilder)
    {


    }
}
