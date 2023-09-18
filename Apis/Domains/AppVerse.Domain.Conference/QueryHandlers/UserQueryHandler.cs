

using AppVerse.Domain.Conference.Dto;
using AppVerse.Domain.Conference.Models;
using AppVerse.Domain.Conference.Queries;
using AutoMapper;

namespace AppVerse.Domain.Conference.QueryHandlers;
public class UserQueryHandler : IQueryStreamHandler<GetConferences, ConferenceModel>
{
    private readonly IRepository repository;
    private readonly IMapper mapper;

    public UserQueryHandler(IRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


    public async IAsyncEnumerable<ConferenceModel> Handle(GetConferences request, CancellationToken cancellationToken)
    {    
        var conferences = repository.GetAll<ConferenceDto>(cancellationToken);
        await foreach (var conference in conferences)
            yield return mapper.Map<ConferenceModel>(conference);
    }
}