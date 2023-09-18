using AppVerse.Domain.Conference.Dto;
using AppVerse.Domain.Conference.Models;
using AutoMapper;

namespace AppVerse.Domain.Conference.MappinProfiles;

internal class ModelDtoProfile:Profile
{
    public ModelDtoProfile()
    {
        CreateMap<ConferenceDto, ConferenceModel>();
    }
}
