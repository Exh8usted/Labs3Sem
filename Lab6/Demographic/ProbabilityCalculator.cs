namespace Demographic;

public static class ProbabilityCalculator
{
    private static readonly Random _random = new Random();
    public static bool CalcProbablility(double eventProbability)
    {
        return _random.NextDouble() <= eventProbability;
    }
    public static double GetRandomDoubleValue()
    {
        return _random.NextDouble();
    }
    public static int GetRandomAge()
    {
        return _random.Next(0, 100);
    }
}

