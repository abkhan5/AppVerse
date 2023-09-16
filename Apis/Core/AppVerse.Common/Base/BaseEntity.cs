using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Base;

public abstract record BaseEntity<T> : BaseEntity
{
    [Key] public virtual T Id { get; set; }
}

public abstract record BaseEntity
{
    public List<BaseDomainEvent> Events = new();
}
public abstract record BaseDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}