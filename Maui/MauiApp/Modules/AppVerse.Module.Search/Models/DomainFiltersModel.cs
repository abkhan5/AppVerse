using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Module.Search.Models;

public record DomainFiltersModel
{

}
public record SearchFiltersModel
{
    public List<DomainFiltersModel> DomainFilters { get; set; }
}

public record BusinesVertical(string DomainDescription, string DomainUrl);

