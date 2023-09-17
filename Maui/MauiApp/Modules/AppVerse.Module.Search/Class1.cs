using AppVerse.Module.Search.Models;
using Refit;

namespace AppVerse.Module.Search;
public class SearchService : ISearchService
{
    private readonly IRealTimeService realTimeService;
    private readonly IMetaSearchService metaSearchService;
    private readonly ISecureStorageService secureStorageService;
    const string FilterKey = "filters";
    public SearchService(IRealTimeService realTimeService, IMetaSearchService metaSearchService, ISecureStorageService secureStorageService)
    {
        this.realTimeService = realTimeService;
        this.metaSearchService = metaSearchService;
        this.secureStorageService = secureStorageService;
    }

    async Task<SearchFiltersModel> ISearchService.GetFilters(CancellationToken cancellationToken)
    {
        var filter = await secureStorageService.GetAsync<SearchFiltersModel>(FilterKey);
        if (filter == null)
        {
            filter = await metaSearchService.GetSearchFilter();
            await secureStorageService.SetAsync(FilterKey, filter);
        }
        return filter;
    }
    async Task ISearchService.RequestSearch(MetaSearchRequestModel request, CancellationToken cancellationToken)
    {
        await realTimeService.Start(cancellationToken);
        await realTimeService.Sender(HubEventNames.SearchRequest, new object?[] { request }, cancellationToken);
    }

    void ISearchService.Subscribe(Func<MetaSearchResponseModel, Task> searchResponseOp, Func<SearchMetadataModel, Task> searchmetadata)
    {
        realTimeService.Receiver(HubEventNames.SearchResponse, searchResponseOp);
        realTimeService.Receiver(HubEventNames.SearcMetadata, searchmetadata);
    }
}
public record RealtimeResponseDto
{
    public string SenderName { get; set; }
    public string Payload { get; set; }
}
public interface ISearchService
{
    Task<SearchFiltersModel> GetFilters(CancellationToken cancellationToken);
    void Subscribe(Func<MetaSearchResponseModel, Task> searchResponseOp, Func<SearchMetadataModel, Task> searchmetadata);
    Task RequestSearch(MetaSearchRequestModel request, CancellationToken cancellationToken);
}


public record SearchOptionsSettings
{
    public const string SearchSetting = "Search";
    public string Host { get; set; }
    public string Filters { get; set; }
}
