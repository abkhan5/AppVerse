namespace AppVerse.Services;

public interface IUniqueCodeGeneratorService
{
    public string GetUniqueCode(string inputString = null);

    public string GetPassword();
}