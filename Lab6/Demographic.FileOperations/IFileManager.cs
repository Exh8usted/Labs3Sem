using System.IO.Compression;

namespace Demographic.FileOperations;

public struct AgeData
{
    public int age;
    public double amount;
    public AgeData(int age, double amount)
    {
        this.age = age;
        this.amount = amount;
    }
    public void Print() => Console.WriteLine($"{age}, {amount}");
}
public struct DeathData
{
    public int primaryAge;
    public int finalAge;
    public double maleDeathProbability;
    public double femaleDeathProbability;
    public void Print() => Console.WriteLine($"{primaryAge}, {finalAge}, {maleDeathProbability}, {femaleDeathProbability}");
    public DeathData(int primaryAge, int finalAge, double maleDeathProbability, double femaleDeathProbability)
    {
        this.primaryAge = primaryAge;
        this.finalAge = finalAge;
        this.maleDeathProbability = maleDeathProbability;
        this.femaleDeathProbability = femaleDeathProbability;
    }
}

public interface IFileManager
{
    public string AgeFilePath { get; set; }
    public string DeathRateFilePath { get; set; }
    public List<AgeData> GetAgeData();
    public List<DeathData> GetDeathData();
    public void CreateAgesResultFile(List<int> MaleAges, List<int> FemaleAges);
    public void CreatePopulationResultFile(List<string> PopulationInString);
}
