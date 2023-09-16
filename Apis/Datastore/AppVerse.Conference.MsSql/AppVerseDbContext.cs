namespace AppVerse.Conference.MsSql;


public class AppVerseDbContext : IdentityDbContext<AppVerseUser>
{
    private readonly IDomainEventDispatcher dispatcher;

    #region Constructor

    public AppVerseDbContext(DbContextOptions<AppVerseDbContext> options) : base(options)
    {
        dispatcher = null;
    }

    #endregion

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        if (dispatcher == null) return result;

        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
                dispatcher.Dispatch(domainEvent);
        }

        return result;
    }

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("AppVerse");
        modelBuilder.ApplyConfiguration(new AppVerseUserMapping());
        modelBuilder.ApplyConfiguration(new ConferenceAgendaMapping());
        modelBuilder.ApplyConfiguration(new ConferenceMapping());
        modelBuilder.ApplyConfiguration(new RefreshTokenMapping());
       

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
