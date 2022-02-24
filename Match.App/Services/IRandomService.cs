namespace Match.App.Services
{
    public interface IRandomService
    {
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }
}