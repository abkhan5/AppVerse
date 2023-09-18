namespace AppVerse.Domain.Conference.Commands;
public record CreateConference(string Agenda, DateTime Start, DateTime End): ICommand;