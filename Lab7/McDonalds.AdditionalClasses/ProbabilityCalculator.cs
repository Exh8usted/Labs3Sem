namespace McDonalds.AdditionalClasses;
public static class ProbabilityCalculator
{
    private static readonly Random _random = new Random();
    public static bool CalcProbablility(double eventProbability)
    {
        return _random.NextDouble() <= eventProbability;
    }
    public static int GetRandomValueInList(List<double> values)
    {
        double random = _random.NextDouble();
        for (int i = 0; i < values.Count; i++)
        {
            if (random < values[i])
            {
                return i;
            }
        }
        return 1;
    }
}
