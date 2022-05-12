namespace WordleWebApi
{
    public interface IWordleService
    {
        string GetRandomWord();
        string? GetRandomWord(string prefix);
    }
}