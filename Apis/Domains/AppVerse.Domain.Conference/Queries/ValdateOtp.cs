using AppVerse.Domain.Conference.Models;

namespace AppVerse.Domain.Conference.Queries;

public record GetConferences() : IQueryStream<ConferenceModel>;