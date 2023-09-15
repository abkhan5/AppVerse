using AppVerse.Database.MsSql.Mappings;

namespace AppVerse.Database.MsSql;
internal class AppVerseDbContext : IdentityDbContext<AppVerseUser>
{
    private readonly IDomainEventDispatcher dispatcher;

    #region Constructor

    public AppVerseDbContext(DbContextOptions<AppVerseDbContext> options) : base(options)
    {
        dispatcher = null;
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("AppVerse");
        modelBuilder.ApplyConfiguration(new AppVerseUserUserMapping());
        modelBuilder.ApplyConfiguration(new RefreshTokenMapping());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        if (dispatcher == null) 
            return result;

        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
            await    dispatcher.Dispatch(domainEvent);
        }

        return result;
    }
}