namespace AppVerse.Api.Authentication.Models;

public record RegisterModelBase
{
    private string firstName;
    private string lastName;


    public string UserEmailId { get; set; }


    public string Password { get; set; }


    public string ConfirmPassword { get; set; }


    public string CountryCode { get; set; }

    public string FirstName
    {
        get => firstName;
        set => firstName = value?.Trim();
    }


    public string LastName
    {
        get => lastName;
        set => lastName = value.Trim();
    }


    public string? ReferalCode { get; set; }


}