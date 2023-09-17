using AppVerse.Module.Search.Models;
using Refit;

namespace AppVerse.Module.Search;

public interface IMetaSearchService
{
    [Get("/MetaSearch/SearchFilters")]
    Task<SearchFiltersModel> GetSearchFilter();
}
