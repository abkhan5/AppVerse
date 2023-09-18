

namespace AppVerse.Domain.Conference.Validation;

public class CreateConferenceValidator : AbstractValidator<CreateConference>
{
    public CreateConferenceValidator()
    {
        RuleFor(x => x.Start).MustAsync(DateIsInFuture).WithErrorCode(AppVerseErrorRegistry.ErrorAuth102);
    }

    private async Task<bool> DateIsInFuture(DateTime time, CancellationToken token)=>   
     time > DateTime.UtcNow;
    
}
